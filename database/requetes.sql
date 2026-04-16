USE GestionInscriptEtudiant;
GO


/*

Par Gaïus Heumen Nkouantchoua
-----------############################# Requêtes utilisant les vues, les procédures stockées, les fonctions, les triggers, les curseurs etc.. ###########

1- Ajouter une vue permettant d'afficher le relevé de notes d'un étudiant
2- Écrire une procédure stockées permettant d'inscrire un étudiant OK
3- Écrire une procédure ou fonction stockée permettant de calculer la moyenne d'un étudiant
4- Écrire une procédure stockée permettant de générer un relevé de notes
5- Écrire une procédure stockée permettant de valider les prerequis
6- Écrire un triggers permettant d'effectuer le recalcul GPA
7- Écrire un triggers permettant d'empêcher une suppression critique
8- Écrire un triggers permettant de vérifier la somme poids d'évaluation
9- Créer une vue affichant la moyenne de note final par étudiant
10-Écrire une fonction stockée affichant les étudiants ayant une moyenne supérieure à une certaine note
11-Écrire une procédure stockée permettant de vérifier la redondance d'une inscription


---- ####################### Requêtes de consultation de base simples (SELECT simples)

1- Listes tous les programmes, les étudiants, les utlisateurs, les enseignants, les administrateurs, les cours, les spécialisations
	un type de cours, étudiants en échec, etc.

----- ##################### Reequêtes de Join essentielles ############################
1- Lister les étudiants d'un programme
2- Donner les spécialisation par programme
3- Les cours  enseignés par un professeur
4- les étudiants inscrits à un cours

----- ##################### Requêtes d'agrégation (COUNT, AVG, SUM, MAX) utilisant les clause GROUP BY, ORDER BY #####################
1- Compter les nombre d'étudiants, le nombre de cours, le nombre d'étudiants par programme
2- Calculer la moyenne générale par étudiant
3- Calculer la moyenne générale d'étudiant par cours
4- Donner le meilleur étudiant par cours
5- Donner l'étudiant ayant la meilleur moyenne générale

----- ############## Sous-requêtes  ###########################
1- Aficher les cours sans inscriptions
2- Afficher les professeurs sans cours

---- ################################ Requêtes complexes ###################################
1- Taux de réussite d'un cours
2- Calculer la charge d'enseignement par professeur
3- Nombre d'étudiants par cours offerts
4- Cours dépassant sa capacité
5- Moyenne d'un évaluation
6- Cours les plus populaires
7- Requêtes permettant d'afficher les cours les plus difficiles
8- Requêtes permettant d'afficher le taux d'abandon
9- Requête permettant d'afficher le nombre d'évaluation par session
10-Requête affichant les étudiants sans notes finales
11-Requêtes affichant les étudiant ayant repris un cours
12- Requêtes affichant le nombre d'inscription par semestre

----- ################ Requêtes de mise à jour (UPDATE) et de suppression (DELETE) #######
1- Effectuer la mise à jour du statut d"un étudiant
2- Effectuer la mise à jour sur la décision finale de réussite ou d'échec d'un édutiant
4- Supprimer les champs de note NULL

*/


/*
	Ajout des rôles de sécurité (à faire)
*/

---######################### Procédure stockées ##############################

-- 1- Écrire une procédure stockée qui permet de créer un nouvel utilisateur
GO
CREATE OR ALTER PROCEDURE CreationCompteUtilisateur 
(
	@nom_User VARCHAR(50),
	@prenom_User VARCHAR(50),
	@dateNais_User DATE,
	@numTel_User VARCHAR(20),
	@passwordHash_User VARCHAR(MAX),
	@courriel_User VARCHAR(100),
	@adresse_User VARCHAR(200),
	@role VARCHAR(100),
	@programme_Etud INT = NULL, --- NULL si l'étudiant n'a pas choisi
	@role_Admin_Etud VARCHAR(100),
	@statut_Etud VARCHAR(80),
	@grade_Prof VARCHAR(50),
	@statut_Prof VARCHAR(30),
	@id_User INT OUTPUT
)
AS
--- Définition de la procédure stockée
BEGIN
	SET NOCOUNT ON -- pour éviter d'afficher les comptes lors des insertions effectuées
	IF EXISTS (
		SELECT 1
		FROM Utilisateur U
		WHERE U.courriel_User = @courriel_User
		)
		BEGIN
			RAISERROR('Cette adresse courriel a déjà été utilisée', 16, 1)
			RETURN;
		END
		INSERT INTO Utilisateur (nom_User, prenom_User, dateNais_User, numTel_User, passwordHash_User, courriel_User, adresse_User )
		VALUES(@nom_User, @prenom_User, @dateNais_User, @numTel_User, @passwordHash_User, @courriel_User, @adresse_User )

		SET @id_User = SCOPE_IDENTITY(); ---- récupération de l'identifiant utilisateur 
		
		----------################### Insertion de rôles ####################----------------

		IF		@role = 'Professeur'
				INSERT INTO Professeur(id_User, grade_Prof, statut_Prof) VALUES(@id_User, @grade_Prof, @statut_Prof);
		ELSE IF @role = 'Administrateur'
				INSERT INTO Administrateur (id_User, role_Admin_Etud) VALUES (@id_User, @role_Admin_Etud);
		ELSE
				INSERT INTO Etudiant (id_User, statut_Etud, programme_Etud) VALUES (@id_User, @statut_Etud, @programme_Etud);
END
GO

-- Ecrire une procédure stockée qui permet d'afficher la liste des programmes et sa hierarchie (Spécialisation + Cours)
-- La procédure va afficher pour la liste des programmes toutes les spécialisations et les informations des cours associées à chaque spécialisation.
-- si le programme n'a pas de spéciaisation, il affichera les cours associés à ce programme ainsi que les informations liés à chaque cours
GO
CREATE OR ALTER PROCEDURE Afficher_Programme_Spec_Cours

AS
BEGIN
	SELECT	P.id_Prog AS IdProg, P.nom_Prog AS NomProg, P.code_Prog AS CodeProg, 
			S.id_Spec AS IdSpec, S.id_Prog_Spec AS IdProgSpec, S.nom_Spec AS NomSpec, S.nbCredit_Spec AS NbCrdSpec,
			C.id_Cours AS IdCours, C.code_Cours AS CodeCours, C.nom_Cours AS NomCours, C.credit_Cours AS CredCours,
			C.type_Cours AS TypeCours, 'AvecSpecialisation' AS TypeLien
	FROM Programme P
	INNER JOIN  Specialisation S
	ON S.id_Prog_Spec = P.id_Prog
	LEFT JOIN Restreindre R
	ON R.id_Spec = S.id_Spec
	LEFT JOIN Cours C
	ON C.id_Cours = R.id_Cours

	UNION ALL
	SELECT	P.id_Prog AS IdProg, P.nom_Prog AS NomProg, P.code_Prog AS CodeProg, CAST(NULL AS INT) AS IdSpec,
			CAST(NULL AS INT) AS IdProgSpec, CAST(NULL AS VARCHAR(30)) AS NomSpec, CAST(NULL AS INT) AS NbCrdSpec,
			CS.id_Cours AS IdCours, CS.code_Cours AS CodeCours, CS.nom_Cours AS NomCours, CS.credit_Cours AS CredCours,
			CS.type_Cours AS TypeCours, 'SansSpecialisation' AS TypeLien		
	FROM Programme P
	LEFT JOIN CoursProgramme CP
	ON CP.id_Prog = P.id_Prog
	LEFT JOIN Cours CS
	ON CS.id_Cours = CP.id_Cours
	WHERE NOT EXISTS (
		SELECT 1
		FROM Specialisation S
		WHERE S.id_Prog_Spec = P.id_Prog 
		);
END
GO


-- Ecrire une procédure stockée qui permet d'afficher la liste des Semestres et sa hierarchie (Sessions d'examen assocciées)
-- La procédure va afficher pour la liste des semestres toutes les sessions d'examen et les informations liée à chaque session d'examens.
 GO
 CREATE OR ALTER PROCEDURE Afficher_Semestre_Sessions

 AS
 BEGIN
	SELECT	SEM.id_Semest AS IdSemest, SEM.nom_Semest AS NomSemest, SEM.datDeb_Semest AS DatDebSemest,
			SEM.annee_Semest AS AnneeSemest, SEM.dateFin_Semest AS DateFinSemest, SE.id_SessExam AS IdSessExam,
			SE.type_SessExam AS TypeSessExam, SE.dateDeb AS DateDeb, SE.dateFin AS DateFin
	FROM Semestre SEM
	LEFT JOIN SessionExamen SE
	ON SE.id_Semest = SEM.id_Semest
	ORDER BY SEM.annee_Semest, SEM.nom_Semest, SE.dateDeb
 END
 GO

 
-- Ecrire une vue qui permet d'afficher La liste des programmes et tous leurs détails
-- Par exemple la vue affiches les informations qui sont propres au programme, affiche 
-- la liste des spécialisations s'il y a lieux ainsi que ceux des cours associés au programme.
-- Cette Procédure utilisera une vue.
GO
CREATE OR ALTER VIEW V_Programme_Details
AS
SELECT	P.id_Prog AS IdProg, P.nom_Prog AS NomProg, code_Prog AS CodeProg, P.dateCreat_Prog AS DateCreatProg,
		P.duree_Prog AS DureeProg, P.nbCredit_Prog AS NbCreditProg, P.unite_Prog AS UniteProg, P.descript_Prog AS DescriptProg,

		----- Sélection sur les spécialisations
		S.id_Spec AS IdSpec, S.nom_Spec AS NomSpec, S.nbCredit_Spec AS NbCreditSpec, S.descript_Spec AS DescriptSpec,
		
		---- Sélection sur les cours
		C.id_Cours AS IdCours, C.nom_Cours AS NomCours, C.code_Cours AS CodeCours, C.nbHeure_Cours AS NbHeureCours, 
		C.credit_Cours AS CreditCours, C.type_Cours AS TypeCours
FROM Programme P
INNER JOIN  Specialisation S
ON S.id_Prog_Spec = P.id_Prog
LEFT JOIN Restreindre R
ON R.id_Spec = S.id_Spec
LEFT JOIN Cours C
ON C.id_Cours = R.id_Cours

	UNION ALL
SELECT		P.id_Prog AS IdProg, P.nom_Prog AS NomProg, code_Prog AS CodeProg, P.dateCreat_Prog AS DateCreatProg,
			P.duree_Prog AS DureeProg, P.nbCredit_Prog AS NbCreditProg, P.unite_Prog AS UniteProg, P.descript_Prog AS DescriptProg,
			
			--- Sélection des spécialisations ---
			CAST(NULL AS INT) AS IdSpec, CAST(NULL AS VARCHAR(30)) AS NomSpec,
			CAST(NULL AS INT) AS NbCrdSpec, CAST(NULL AS VARCHAR(400)) AS DescriptSpec,

			---- Sélection sur les cours
			C.id_Cours AS IdCours, C.nom_Cours AS NomCours, C.code_Cours AS CodeCours, C.nbHeure_Cours AS NbHeureCours, 
			C.credit_Cours AS CreditCours, C.type_Cours AS TypeCours
			
FROM Programme P
LEFT JOIN CoursProgramme CP
ON CP.id_Prog = P.id_Prog
LEFT JOIN Cours C
ON C.id_Cours = CP.id_Cours
WHERE NOT EXISTS (
	SELECT 1
	FROM Specialisation S
	WHERE S.id_Prog_Spec = P.id_Prog 
	);

GO

-- Ecrire une procédure stockée qui permet d'afficher un programme et tous ses détails
-- Par exemple le programme doit contenir toutes les informations qui lui sont propres, affiche 
-- la liste des spécialisations s'il y a lieux ainsi que ceux des cours associés au programme.
-- Cette Procédure utilisera une vue.

GO
CREATE OR ALTER PROCEDURE Programme_Detail

	@IdProg INT

AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM V_Programme_Details
	WHERE IdProg = @IdProg
END
GO

 Go
 EXEC Afficher_Programme_Spec_Cours
 GO

 Go
 EXEC Afficher_Semestre_Sessions
 GO

 GO
 EXEC Programme_Detail 1008
 GO