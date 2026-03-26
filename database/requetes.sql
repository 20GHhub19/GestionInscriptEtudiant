USE GestionInscriptEtudiant;
GO

/*
 =====================================================================
  REQUÊTES SQL — GestionInscriptEtudiant
  5 requêtes simples + 5 requêtes complexes (4+ relations chacune)
 =====================================================================
*/

-- ======================= REQUÊTES SIMPLES ==========================

-- R1) Lister toutes les notes attribuées avec la note en lettre et la rétroaction
SELECT id_Note,
       valNum_Note,
       ValLettre_Note,
       retroAction,
       dateAttrib_Note
FROM Note;

-- R2) Lister les cours restreints à une spécialisation donnée
--     (affiche le nombre de places réservées par restriction)
SELECT R.id_Rest,
       C.code_Cours,
       C.nom_Cours,
       S.nom_Spec,
       R.nb_Restrict
FROM Restreindre R
JOIN Cours C ON R.id_Cours = C.id_Cours
JOIN Specialisation S ON R.id_Spec = S.id_Spec;

-- R3) Lister les affectations d'enseignement : professeur, heures et période
SELECT E.id_Enseigner,
       P.nom_Prof,
       P.prenom_Prof,
       E.nbH_Enseigner,
       E.datDeb_Enseigner,
       E.dateFin_Enseigner
FROM Enseigner E
JOIN Professeur P ON E.id_Prof = P.id_Prof;

-- R4) Nombre total d'heures d'enseignement par professeur
SELECT P.id_Prof,
       P.nom_Prof,
       P.prenom_Prof,
       SUM(E.nbH_Enseigner) AS totalHeures
FROM Enseigner E
JOIN Professeur P ON E.id_Prof = P.id_Prof
GROUP BY P.id_Prof, P.nom_Prof, P.prenom_Prof;

-- R5) Moyenne des notes numériques par évaluation
SELECT N.id_Eval,
       Ev.nom_Eval,
       COUNT(N.id_Note) AS nbNotes,
       AVG(N.valNum_Note) AS moyenneNote,
       MIN(N.valNum_Note) AS noteMin,
       MAX(N.valNum_Note) AS noteMax
FROM Note N
JOIN Evaluation Ev ON N.id_Eval = Ev.id_Eval
GROUP BY N.id_Eval, Ev.nom_Eval;


-- ===================== REQUÊTES COMPLEXES ==========================
-- (chacune implique au moins 4 relations)

-- R6) Bulletin complet d'un étudiant : ses notes par évaluation,
--     avec le nom du cours, le semestre et le type d'évaluation
--     Relations : Note, Inscription, Etudiant, Evaluation, CoursOffert, Cours, Semestre (7)
SELECT Et.mat_Etud,
       Et.nom_Etud,
       Et.prenom_Etud,
       C.code_Cours,
       C.nom_Cours,
       Sem.nom_Semest,
       Sem.annee_Semest,
       Ev.nom_Eval,
       Ev.type_Eval,
       Ev.poids_Eval,
       N.valNum_Note,
       N.ValLettre_Note
FROM Note N
JOIN Inscription I   ON N.id_Inscript = I.id_Inscript
JOIN Etudiant Et     ON I.id_Etud = Et.id_Etud
JOIN Evaluation Ev   ON N.id_Eval = Ev.id_Eval
JOIN CoursOffert CO  ON I.id_CoursOf = CO.id_CoursOf
JOIN Cours C         ON CO.id_Cours = C.id_Cours
JOIN Semestre Sem    ON CO.id_Semest = Sem.id_Semest
ORDER BY Et.mat_Etud, Sem.annee_Semest, C.code_Cours, Ev.date_Eval;

-- R7) Moyenne pondérée finale par étudiant et par cours offert,
--     en tenant compte du poids de chaque évaluation
--     Relations : Note, Inscription, Etudiant, Evaluation, CoursOffert, Cours (6)
SELECT Et.mat_Etud,
       Et.nom_Etud,
       Et.prenom_Etud,
       C.code_Cours,
       C.nom_Cours,
       ROUND(
           SUM(N.valNum_Note * Ev.poids_Eval) / NULLIF(SUM(Ev.poids_Eval), 0),
           2
       ) AS moyennePonderee
FROM Note N
JOIN Inscription I   ON N.id_Inscript = I.id_Inscript
JOIN Etudiant Et     ON I.id_Etud = Et.id_Etud
JOIN Evaluation Ev   ON N.id_Eval = Ev.id_Eval
JOIN CoursOffert CO  ON I.id_CoursOf = CO.id_CoursOf
JOIN Cours C         ON CO.id_Cours = C.id_Cours
GROUP BY Et.mat_Etud, Et.nom_Etud, Et.prenom_Etud, C.code_Cours, C.nom_Cours;

-- R8) Liste des professeurs et les cours qu'ils enseignent,
--     avec le programme associé, le semestre et la salle
--     Relations : Enseigner, Professeur, CoursOffert, Cours, CoursProgramme, Programme, Semestre (7)
SELECT P.code_Prof,
       P.nom_Prof,
       P.prenom_Prof,
       P.grade_Prof,
       C.code_Cours,
       C.nom_Cours,
       Prog.nom_Prog,
       Sem.nom_Semest,
       Sem.annee_Semest,
       CO.salle_CoursOf,
       CO.horaire_CoursOf,
       E.nbH_Enseigner
FROM Enseigner E
JOIN Professeur P     ON E.id_Prof = P.id_Prof
JOIN CoursOffert CO   ON E.id_CoursOf = CO.id_CoursOf
JOIN Cours C          ON CO.id_Cours = C.id_Cours
JOIN Semestre Sem     ON CO.id_Semest = Sem.id_Semest
JOIN CoursProgramme CP ON C.id_Cours = CP.id_Cours
JOIN Programme Prog   ON CP.id_Prog = Prog.id_Prog
ORDER BY P.nom_Prof, Sem.annee_Semest;

-- R9) Cours restreints par spécialisation avec le programme parent,
--     le nombre de crédits du cours et la capacité du cours offert
--     Relations : Restreindre, Specialisation, Programme, Cours, CoursOffert, Semestre (6)
SELECT Prog.nom_Prog,
       Sp.nom_Spec,
       C.code_Cours,
       C.nom_Cours,
       C.credit_Cours,
       R.nb_Restrict,
       CO.capacite_CoursOf,
       CO.horaire_CoursOf,
       Sem.nom_Semest,
       Sem.annee_Semest
FROM Restreindre R
JOIN Specialisation Sp ON R.id_Spec = Sp.id_Spec
JOIN Programme Prog    ON Sp.id_Prog_Spec = Prog.id_Prog
JOIN Cours C           ON R.id_Cours = C.id_Cours
JOIN CoursOffert CO    ON C.id_Cours = CO.id_Cours
JOIN Semestre Sem      ON CO.id_Semest = Sem.id_Semest
ORDER BY Prog.nom_Prog, Sp.nom_Spec, C.code_Cours;

-- R10) Résumé de session : pour chaque session d'examen, afficher les évaluations,
--      le cours concerné, le professeur responsable et la moyenne des notes obtenues
--      Relations : SessionExamen, Semestre, Evaluation, CoursOffert, Cours, Enseigner, Professeur, Note (8)
SELECT Sem.nom_Semest,
       Sem.annee_Semest,
       SE.type_SessExam,
       SE.dateDeb,
       C.code_Cours,
       C.nom_Cours,
       Ev.nom_Eval,
       Ev.type_Eval,
       P.nom_Prof,
       P.prenom_Prof,
       COUNT(N.id_Note) AS nbNotes,
       AVG(N.valNum_Note) AS moyenneNotes
FROM SessionExamen SE
JOIN Semestre Sem     ON SE.id_Semest = Sem.id_Semest
JOIN Evaluation Ev    ON Ev.id_SessExam = SE.id_SessExam
JOIN CoursOffert CO   ON Ev.id_CoursOf = CO.id_CoursOf
JOIN Cours C          ON CO.id_Cours = C.id_Cours
JOIN Enseigner E      ON E.id_CoursOf = CO.id_CoursOf
JOIN Professeur P     ON E.id_Prof = P.id_Prof
LEFT JOIN Note N      ON N.id_Eval = Ev.id_Eval
GROUP BY Sem.nom_Semest, Sem.annee_Semest, SE.type_SessExam, SE.dateDeb,
         C.code_Cours, C.nom_Cours, Ev.nom_Eval, Ev.type_Eval,
         P.nom_Prof, P.prenom_Prof
ORDER BY Sem.annee_Semest, SE.dateDeb, C.code_Cours;
