/*
 =====================================================================
  T-SQL — GestionInscriptEtudiant
  Déclencheurs, fonctions, procédures stockées et curseurs
 =====================================================================
  Ordre d'exécution : après LDD_LMD.sql et requetes.sql
  Prérequis : la base GestionInscriptEtudiant doit exister et contenir
              les tables, les contraintes ainsi que les données
              de test insérées via LDD_LMD.sql.
 =====================================================================
*/

USE GestionInscriptEtudiant;
GO


/* =====================================================================
   1) FONCTIONS
   ===================================================================== */

/* --------------------------------------------------------------------
   F1 — fn_MoyennePondereeEtudiantCours
   Calcule la moyenne pondérée d'un étudiant pour un cours offert
   donné, à partir de la table Note et du poids de chaque évaluation.
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.fn_MoyennePondereeEtudiantCours', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_MoyennePondereeEtudiantCours;
GO

CREATE FUNCTION dbo.fn_MoyennePondereeEtudiantCours
(
    @id_Etud    INT,
    @id_CoursOf INT
)
RETURNS DECIMAL(5, 2)
AS
BEGIN
    DECLARE @moyenne DECIMAL(5, 2);

    SELECT @moyenne = CAST(
        SUM(N.valNum_Note * Ev.poids_Eval) /
        NULLIF(SUM(Ev.poids_Eval), 0)
        AS DECIMAL(5, 2))
    FROM Note N
    JOIN Inscription I ON N.id_Inscript = I.id_Inscript
    JOIN Evaluation  Ev ON N.id_Eval     = Ev.id_Eval
    WHERE I.id_Etud   = @id_Etud
      AND I.id_CoursOf = @id_CoursOf;

    RETURN ISNULL(@moyenne, 0);
END;
GO


/* --------------------------------------------------------------------
   F2 — fn_NoteLettre
   Convertit une note numérique (sur 20) en note lettre selon le
   barème officiel de l'Université de Montréal.
     A+ ≥ 19   A ≥ 18   A- ≥ 17
     B+ ≥ 16   B ≥ 15   B- ≥ 14
     C+ ≥ 13   C ≥ 12   C- ≥ 11
     D+ ≥ 10   D ≥ 9
     E  ≥ 7    F  < 7
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.fn_NoteLettre', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_NoteLettre;
GO

CREATE FUNCTION dbo.fn_NoteLettre (@note DECIMAL(5, 2))
RETURNS VARCHAR(2)
AS
BEGIN
    RETURN CASE
        WHEN @note IS NULL     THEN NULL
        WHEN @note >= 19.00    THEN 'A+'
        WHEN @note >= 18.00    THEN 'A'
        WHEN @note >= 17.00    THEN 'A-'
        WHEN @note >= 16.00    THEN 'B+'
        WHEN @note >= 15.00    THEN 'B'
        WHEN @note >= 14.00    THEN 'B-'
        WHEN @note >= 13.00    THEN 'C+'
        WHEN @note >= 12.00    THEN 'C'
        WHEN @note >= 11.00    THEN 'C-'
        WHEN @note >= 10.00    THEN 'D+'
        WHEN @note >= 9.00     THEN 'D'
        WHEN @note >= 7.00     THEN 'E'
        ELSE 'F'
    END;
END;
GO


/* --------------------------------------------------------------------
   F3 — fn_EtudiantsAuDessusMoyenne (Inline Table-Valued Function)
   Retourne la liste des étudiants dont la moyenne pondérée finale
   est supérieure ou égale à un seuil donné.
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.fn_EtudiantsAuDessusMoyenne', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_EtudiantsAuDessusMoyenne;
GO

CREATE FUNCTION dbo.fn_EtudiantsAuDessusMoyenne (@seuil DECIMAL(5, 2))
RETURNS TABLE
AS
RETURN
(
    SELECT U.id_User,
           U.mat_User,
           U.nom_User,
           U.prenom_User,
           CAST(
               SUM(N.valNum_Note * Ev.poids_Eval) /
               NULLIF(SUM(Ev.poids_Eval), 0)
               AS DECIMAL(5, 2)) AS moyenne
    FROM Utilisateur U
    JOIN Etudiant Et    ON U.id_User = Et.id_User
    JOIN Inscription I  ON Et.id_User = I.id_Etud
    JOIN Note N         ON I.id_Inscript = N.id_Inscript
    JOIN Evaluation Ev  ON N.id_Eval = Ev.id_Eval
    GROUP BY U.id_User, U.mat_User, U.nom_User, U.prenom_User
    HAVING  CAST(
                SUM(N.valNum_Note * Ev.poids_Eval) /
                NULLIF(SUM(Ev.poids_Eval), 0)
                AS DECIMAL(5, 2)) >= @seuil
);
GO


/* =====================================================================
   2) PROCÉDURES STOCKÉES
   ===================================================================== */

/* --------------------------------------------------------------------
   P1 — sp_InscrireEtudiantCours
   Inscrit un étudiant à un cours offert en vérifiant :
     - l'existence de l'étudiant et du cours offert
     - la capacité restante du cours offert
     - l'absence de doublon (contrainte UNIQUE déjà en place)
     - la validation des prérequis via CoursPrerequis
   Utilise une transaction pour garantir la cohérence.
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.sp_InscrireEtudiantCours', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_InscrireEtudiantCours;
GO

CREATE PROCEDURE dbo.sp_InscrireEtudiantCours
(
    @id_Etud    INT,
    @id_CoursOf INT,
    @id_Inscript INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Etudiant WHERE id_User = @id_Etud)
        BEGIN
            RAISERROR('Étudiant introuvable.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        DECLARE @capacite INT, @inscrits INT, @id_Cours INT;

        SELECT @capacite = capacite_CoursOf,
               @id_Cours = id_Cours
        FROM   CoursOffert
        WHERE  id_CoursOf = @id_CoursOf;

        IF @capacite IS NULL
        BEGIN
            RAISERROR('Cours offert introuvable.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        SELECT @inscrits = COUNT(*)
        FROM   Inscription
        WHERE  id_CoursOf = @id_CoursOf
          AND  statut_Inscript = 'Inscrit';

        IF @inscrits >= @capacite
        BEGIN
            RAISERROR('Capacité maximale atteinte pour ce cours offert.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validation des prérequis : l'étudiant doit avoir réussi
        -- tous les cours prérequis du cours cible.
        DECLARE @prerequisManquants INT = 0;

        SELECT @prerequisManquants = COUNT(*)
        FROM   CoursPrerequis CP
        WHERE  CP.id_Cours = @id_Cours
          AND  NOT EXISTS (
                SELECT 1
                FROM   Inscription I
                JOIN   CoursOffert CO ON I.id_CoursOf = CO.id_CoursOf
                WHERE  I.id_Etud = @id_Etud
                  AND  CO.id_Cours = CP.id_Prerequis
                  AND  I.decisionFi_Inscript = 'Réussi'
              );

        DECLARE @prerequisOk BIT =
            CASE WHEN @prerequisManquants = 0 THEN 1 ELSE 0 END;

        INSERT INTO Inscription
            (statut_Inscript, date_Inscript, tentative_Inscript,
             estValidePrerequis_Inscript, id_Etud, id_CoursOf)
        VALUES
            ('Inscrit', GETDATE(), 1, @prerequisOk, @id_Etud, @id_CoursOf);

        SET @id_Inscript = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO


/* --------------------------------------------------------------------
   P2 — sp_ReleveNotesEtudiant
   Génère le relevé de notes d'un étudiant : pour chaque cours suivi,
   affiche la moyenne pondérée et la note lettre correspondante.
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.sp_ReleveNotesEtudiant', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ReleveNotesEtudiant;
GO

CREATE PROCEDURE dbo.sp_ReleveNotesEtudiant (@id_Etud INT)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT U.mat_User,
           U.nom_User,
           U.prenom_User,
           Sem.nom_Semest,
           Sem.annee_Semest,
           C.code_Cours,
           C.nom_Cours,
           C.credit_Cours,
           dbo.fn_MoyennePondereeEtudiantCours(I.id_Etud, I.id_CoursOf) AS moyenne,
           dbo.fn_NoteLettre(
               dbo.fn_MoyennePondereeEtudiantCours(I.id_Etud, I.id_CoursOf)
           ) AS noteLettre,
           I.decisionFi_Inscript
    FROM   Inscription I
    JOIN   Etudiant    Et  ON I.id_Etud    = Et.id_User
    JOIN   Utilisateur U   ON Et.id_User   = U.id_User
    JOIN   CoursOffert CO  ON I.id_CoursOf = CO.id_CoursOf
    JOIN   Cours       C   ON CO.id_Cours  = C.id_Cours
    JOIN   Semestre    Sem ON CO.id_Semest = Sem.id_Semest
    WHERE  I.id_Etud = @id_Etud
    ORDER BY Sem.annee_Semest, Sem.nom_Semest, C.code_Cours;
END;
GO


/* --------------------------------------------------------------------
   P3 — sp_MajNoteLettreInscription (utilise un CURSEUR)
   Parcourt toutes les inscriptions où la note finale numérique est
   renseignée mais la note lettre manquante ou incohérente, et met à
   jour noteLet_Inscript à partir de la fonction fn_NoteLettre.
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.sp_MajNoteLettreInscription', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_MajNoteLettreInscription;
GO

CREATE PROCEDURE dbo.sp_MajNoteLettreInscription
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id_Inscript INT;
    DECLARE @noteFi DECIMAL(4, 2);
    DECLARE @nouvelleLettre VARCHAR(2);
    DECLARE @nbMaj INT = 0;

    DECLARE cur_Inscript CURSOR LOCAL FAST_FORWARD FOR
        SELECT id_Inscript, noteFi_Inscript
        FROM   Inscription
        WHERE  noteFi_Inscript IS NOT NULL;

    OPEN cur_Inscript;
    FETCH NEXT FROM cur_Inscript INTO @id_Inscript, @noteFi;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @nouvelleLettre = dbo.fn_NoteLettre(@noteFi);

        UPDATE Inscription
        SET    noteLet_Inscript = @nouvelleLettre,
               decisionFi_Inscript =
                   CASE WHEN @noteFi >= 10 THEN 'Réussi' ELSE 'Échec' END
        WHERE  id_Inscript = @id_Inscript
          AND  (noteLet_Inscript IS NULL
                OR noteLet_Inscript <> @nouvelleLettre);

        SET @nbMaj = @nbMaj + @@ROWCOUNT;

        FETCH NEXT FROM cur_Inscript INTO @id_Inscript, @noteFi;
    END

    CLOSE cur_Inscript;
    DEALLOCATE cur_Inscript;

    PRINT CONCAT('Inscriptions mises à jour : ', @nbMaj);
END;
GO


/* =====================================================================
   3) DÉCLENCHEURS (TRIGGERS)
   ===================================================================== */

/* --------------------------------------------------------------------
   T1 — trg_Note_RecalculerInscription
   Après INSERT/UPDATE/DELETE sur Note, recalcule automatiquement la
   moyenne pondérée de l'inscription concernée ainsi que la note
   lettre et la décision finale (Réussi / Échec).
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.trg_Note_RecalculerInscription', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Note_RecalculerInscription;
GO

CREATE TRIGGER dbo.trg_Note_RecalculerInscription
ON  dbo.Note
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1) Recalcul de la lettre individuelle de chaque note touchée.
    --    (Ne fait rien en cas de DELETE pur.)
    UPDATE N
    SET    N.ValLettre_Note = dbo.fn_NoteLettre(N.valNum_Note)
    FROM   Note N
    JOIN   inserted I ON N.id_Note = I.id_Note
    WHERE  N.ValLettre_Note IS NULL
        OR N.ValLettre_Note <> dbo.fn_NoteLettre(N.valNum_Note);

    -- 2) On rassemble toutes les inscriptions impactées pour recalculer
    --    la moyenne pondérée du cours.
    DECLARE @inscriptions TABLE (id_Inscript INT PRIMARY KEY);

    INSERT INTO @inscriptions (id_Inscript)
    SELECT DISTINCT id_Inscript FROM inserted
    UNION
    SELECT DISTINCT id_Inscript FROM deleted;

    -- Recalcul par inscription.
    UPDATE I
    SET    I.noteFi_Inscript   = X.moyenne,
           I.noteLet_Inscript  = dbo.fn_NoteLettre(X.moyenne),
           I.decisionFi_Inscript =
               CASE
                   WHEN X.moyenne IS NULL      THEN I.decisionFi_Inscript
                   WHEN X.moyenne >= 10        THEN 'Réussi'
                   ELSE 'Échec'
               END
    FROM   Inscription I
    JOIN   @inscriptions A ON I.id_Inscript = A.id_Inscript
    OUTER APPLY (
        SELECT CAST(
                   SUM(N.valNum_Note * Ev.poids_Eval) /
                   NULLIF(SUM(Ev.poids_Eval), 0)
                   AS DECIMAL(5, 2)) AS moyenne
        FROM   Note N
        JOIN   Evaluation Ev ON N.id_Eval = Ev.id_Eval
        WHERE  N.id_Inscript = I.id_Inscript
    ) AS X;
END;
GO


/* --------------------------------------------------------------------
   T2 — trg_Evaluation_VerifierPoids
   Empêche INSERT ou UPDATE d'une évaluation qui ferait dépasser 100
   la somme des poids des évaluations d'un même cours offert.
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.trg_Evaluation_VerifierPoids', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Evaluation_VerifierPoids;
GO

CREATE TRIGGER dbo.trg_Evaluation_VerifierPoids
ON  dbo.Evaluation
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM   Evaluation E
        JOIN   inserted   I ON E.id_CoursOf = I.id_CoursOf
        GROUP BY E.id_CoursOf
        HAVING  SUM(E.poids_Eval) > 100.00
    )
    BEGIN
        RAISERROR(
            'La somme des poids des évaluations d''un cours offert ne peut pas dépasser 100.',
            16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO


/* --------------------------------------------------------------------
   T3 — trg_Inscription_EmpecherSuppressionAvecNotes
   Empêche la suppression d'une Inscription qui possède encore des
   notes associées — préserve l'intégrité du dossier étudiant.
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.trg_Inscription_EmpecherSuppressionAvecNotes', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Inscription_EmpecherSuppressionAvecNotes;
GO

CREATE TRIGGER dbo.trg_Inscription_EmpecherSuppressionAvecNotes
ON  dbo.Inscription
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM   Note N
        JOIN   deleted D ON N.id_Inscript = D.id_Inscript
    )
    BEGIN
        RAISERROR(
            'Suppression interdite : des notes sont encore rattachées à cette inscription.',
            16, 1);
        RETURN;
    END

    DELETE I
    FROM   Inscription I
    JOIN   deleted     D ON I.id_Inscript = D.id_Inscript;
END;
GO


/* =====================================================================
   4) VUE
   ===================================================================== */

/* --------------------------------------------------------------------
   V1 — v_MoyenneFinaleEtudiant
   Moyenne pondérée finale par étudiant (tous cours confondus).
   -------------------------------------------------------------------- */
IF OBJECT_ID('dbo.v_MoyenneFinaleEtudiant', 'V') IS NOT NULL
    DROP VIEW dbo.v_MoyenneFinaleEtudiant;
GO

CREATE VIEW dbo.v_MoyenneFinaleEtudiant
AS
    SELECT U.mat_User,
           U.nom_User,
           U.prenom_User,
           CAST(
               SUM(N.valNum_Note * Ev.poids_Eval) /
               NULLIF(SUM(Ev.poids_Eval), 0)
               AS DECIMAL(5, 2)) AS moyenneGenerale,
           COUNT(DISTINCT I.id_CoursOf) AS nbCoursSuivis
    FROM   Utilisateur U
    JOIN   Etudiant    Et ON U.id_User    = Et.id_User
    JOIN   Inscription I  ON Et.id_User   = I.id_Etud
    JOIN   Note        N  ON I.id_Inscript = N.id_Inscript
    JOIN   Evaluation  Ev ON N.id_Eval     = Ev.id_Eval
    GROUP BY U.mat_User, U.nom_User, U.prenom_User;
GO


/* =====================================================================
   5) TESTS RAPIDES — à exécuter pour valider chaque objet
   ===================================================================== */
/*
-- Fonctions
SELECT dbo.fn_MoyennePondereeEtudiantCours(100, 5)                AS moyCours;
SELECT dbo.fn_NoteLettre(15.50)                                   AS lettre;
SELECT * FROM dbo.fn_EtudiantsAuDessusMoyenne(12.00);

-- Procédure d'inscription (capacité & prérequis)
DECLARE @nouvelId INT;
EXEC dbo.sp_InscrireEtudiantCours
     @id_Etud = 102, @id_CoursOf = 4, @id_Inscript = @nouvelId OUTPUT;
SELECT @nouvelId AS nouveauId_Inscript;

-- Relevé de notes
EXEC dbo.sp_ReleveNotesEtudiant @id_Etud = 100;

-- Curseur : recalcul des lettres
EXEC dbo.sp_MajNoteLettreInscription;

-- Trigger T1 : mise à jour automatique après insertion d'une note
-- INSERT INTO Note (valNum_Note, ValLettre_Note, dateAttrib_Note, id_Inscript, id_Eval)
-- VALUES (17.5, 'B', GETDATE(), 10, 1);
-- SELECT noteFi_Inscript, noteLet_Inscript FROM Inscription WHERE id_Inscript = 10;

-- Trigger T2 : tentative d'insérer une évaluation dépassant 100
-- INSERT INTO Evaluation (type_Eval, date_Eval, nom_Eval, poids_Eval, id_CoursOf, id_SessExam)
-- VALUES ('Quiz', '2024-03-01', 'Quiz massif', 50.00, 1, 1);
-- -> doit lever une erreur (la somme dépasse déjà 85 pour le cours offert 1)

-- Trigger T3 : tentative de suppression d'une inscription notée
-- DELETE FROM Inscription WHERE id_Inscript = 10;  -- doit lever une erreur

-- Vue
SELECT TOP 10 * FROM dbo.v_MoyenneFinaleEtudiant ORDER BY moyenneGenerale DESC;
*/
