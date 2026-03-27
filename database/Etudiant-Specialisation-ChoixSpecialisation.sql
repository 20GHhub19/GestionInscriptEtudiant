USE GestionInscriptEtudiant;
GO

/* =========================================================
   REQU�TE 1
   Afficher la liste des �tudiants avec leur programme
   et leur sp�cialisation choisie.
   Relations : Utilisateur, Etudiant, Programme,
               ChoixSpecialisation, Specialisation
   --> 5 relations
========================================================= */
SELECT 'REQU�TE 1 : Afficher la liste des �tudiants avec leur programme et leur sp�cialisation choisie. Relations : Utilisateur, Etudiant, Programme, ChoixSpecialisation, Specialisation.' AS Info;

SELECT 
    u.id_User AS id_Etudiant,
    u.nom_User,
    u.prenom_User,
    e.statut_Etud,
    p.nom_Prog,
    s.nom_Spec,
    cs.date_ChoixSpec
FROM Utilisateur u
INNER JOIN Etudiant e
    ON u.id_User = e.id_User
INNER JOIN Programme p
    ON e.programme_Etud = p.id_Prog
LEFT JOIN ChoixSpecialisation cs
    ON e.id_User = cs.id_Etud
LEFT JOIN Specialisation s
    ON cs.id_Spec = s.id_Spec
ORDER BY u.nom_User, u.prenom_User;
GO


/* =========================================================
   REQU�TE 2
   Afficher les cours offerts avec leur programme associ�
   et le nombre d'heures.
   Relations : CoursOffert, Cours, CoursProgramme, Programme
   --> 4 relations
========================================================= */
SELECT 'REQU�TE 2 : Afficher les cours offerts avec leur programme associ� et le nombre d''heures. Relations : CoursOffert, Cours, CoursProgramme, Programme.' AS Info;

SELECT 
    co.id_CoursOf,
    c.code_Cours,
    c.nom_Cours,
    p.nom_Prog,
    co.horaire_CoursOf,
    co.salle_CoursOf,
    c.nbHeure_Cours
FROM CoursOffert co
INNER JOIN Cours c
    ON co.id_Cours = c.id_Cours
INNER JOIN CoursProgramme cp
    ON c.id_Cours = cp.id_Cours
INNER JOIN Programme p
    ON cp.id_Prog = p.id_Prog
ORDER BY p.nom_Prog, c.nom_Cours;
GO


/* =========================================================
   REQU�TE 3
   Afficher les �tudiants inscrits � un cours offert,
   avec le cours et leur r�sultat final.
   Relations : Utilisateur, Etudiant, Inscription,
               CoursOffert, Cours
   --> 5 relations
========================================================= */
SELECT 'REQU�TE 3 : Afficher les �tudiants inscrits � un cours offert, avec le cours et leur r�sultat final. Relations : Utilisateur, Etudiant, Inscription, CoursOffert, Cours.' AS Info;

SELECT 
    u.nom_User,
    u.prenom_User,
    c.nom_Cours,
    i.statut_Inscript,
    i.noteFi_Inscript,
    i.noteLet_Inscript,
    i.decisionFi_Inscript
FROM Utilisateur u
INNER JOIN Etudiant e
    ON u.id_User = e.id_User
INNER JOIN Inscription i
    ON e.id_User = i.id_Etud
INNER JOIN CoursOffert co
    ON i.id_CoursOf = co.id_CoursOf
INNER JOIN Cours c
    ON co.id_Cours = c.id_Cours
ORDER BY u.nom_User, c.nom_Cours;
GO


/* =========================================================
   REQU�TE 4
   Afficher les �valuations avec le cours offert
   et la session d'examen correspondante.
   Relations : Evaluation, CoursOffert, Cours,
               SessionExamen, Semestre
   --> 5 relations
========================================================= */
SELECT 'REQU�TE 4 : Afficher les �valuations avec le cours offert et la session d''examen correspondante. Relations : Evaluation, CoursOffert, Cours, SessionExamen, Semestre.' AS Info;

SELECT 
    ev.nom_Eval,
    ev.type_Eval,
    ev.date_Eval,
    ev.poids_Eval,
    c.nom_Cours,
    se.type_SessExam,
    sem.nom_Semest,
    sem.annee_Semest
FROM Evaluation ev
INNER JOIN CoursOffert co
    ON ev.id_CoursOf = co.id_CoursOf
INNER JOIN Cours c
    ON co.id_Cours = c.id_Cours
INNER JOIN SessionExamen se
    ON ev.id_SessExam = se.id_SessExam
INNER JOIN Semestre sem
    ON se.id_Semest = sem.id_Semest
ORDER BY sem.annee_Semest, sem.nom_Semest, c.nom_Cours;
GO


/* =========================================================
   REQU�TE 5
   Afficher les notes obtenues par chaque �tudiant
   pour chaque �valuation.
   Relations : Note, Inscription, Utilisateur, Etudiant,
               Evaluation, CoursOffert, Cours
   --> 7 relations
========================================================= */
SELECT 'REQU�TE 5 : Afficher les notes obtenues par chaque �tudiant pour chaque �valuation. Relations : Note, Inscription, Utilisateur, Etudiant, Evaluation, CoursOffert, Cours.' AS Info;

SELECT 
    u.nom_User,
    u.prenom_User,
    c.nom_Cours,
    ev.nom_Eval,
    ev.type_Eval,
    n.valNum_Note,
    n.ValLettre_Note,
    n.retroAction
FROM Note n
INNER JOIN Inscription i
    ON n.id_Inscript = i.id_Inscript
INNER JOIN Etudiant e
    ON i.id_Etud = e.id_User
INNER JOIN Utilisateur u
    ON e.id_User = u.id_User
INNER JOIN Evaluation ev
    ON n.id_Eval = ev.id_Eval
INNER JOIN CoursOffert co
    ON i.id_CoursOf = co.id_CoursOf
INNER JOIN Cours c
    ON co.id_Cours = c.id_Cours
ORDER BY u.nom_User, c.nom_Cours, ev.nom_Eval;
GO


/* =========================================================
   REQU�TE 6
   Afficher les professeurs avec les cours qu'ils enseignent.
   Relations : Professeur, Utilisateur, Enseigner,
               CoursOffert, Cours
   --> 5 relations
========================================================= */
SELECT 'REQU�TE 6 : Afficher les professeurs avec les cours qu''ils enseignent. Relations : Professeur, Utilisateur, Enseigner, CoursOffert, Cours.' AS Info;

SELECT 
    u.nom_User,
    u.prenom_User,
    p.grade_Prof,
    p.statut_Prof,
    c.nom_Cours,
    co.horaire_CoursOf,
    en.nbH_Enseigner
FROM Professeur p
INNER JOIN Utilisateur u
    ON p.id_User = u.id_User
INNER JOIN Enseigner en
    ON p.id_User = en.id_Prof
INNER JOIN CoursOffert co
    ON en.id_CoursOf = co.id_CoursOf
INNER JOIN Cours c
    ON co.id_Cours = c.id_Cours
ORDER BY u.nom_User, c.nom_Cours;
GO


/* =========================================================
   REQU�TE 7
   Afficher les cours restreints selon la sp�cialisation.
   Relations : Restreindre, Specialisation, Cours
   --> 3 relations
========================================================= */
SELECT 'REQU�TE 7 : Afficher les cours restreints selon la sp�cialisation. Relations : Restreindre, Specialisation, Cours.' AS Info;

SELECT 
    s.nom_Spec,
    c.code_Cours,
    c.nom_Cours,
    r.nb_Restrict
FROM Restreindre r
INNER JOIN Specialisation s
    ON r.id_Spec = s.id_Spec
INNER JOIN Cours c
    ON r.id_Cours = c.id_Cours
ORDER BY s.nom_Spec, c.nom_Cours;
GO


/* =========================================================
   REQU�TE 8
   Calculer la moyenne finale des �tudiants par programme.
   Relations : Programme, Etudiant, Inscription
   --> 3 relations
========================================================= */
SELECT 'REQU�TE 8 : Calculer la moyenne finale des �tudiants par programme. Relations : Programme, Etudiant, Inscription.' AS Info;

SELECT 
    p.nom_Prog,
    COUNT(DISTINCT e.id_User) AS nb_Etudiants,
    AVG(i.noteFi_Inscript) AS moyenne_Programme
FROM Programme p
INNER JOIN Etudiant e
    ON p.id_Prog = e.programme_Etud
INNER JOIN Inscription i
    ON e.id_User = i.id_Etud
GROUP BY p.nom_Prog
ORDER BY moyenne_Programme DESC;
GO


/* =========================================================
   REQU�TE 9
   Afficher les �tudiants qui ont r�ussi tous leurs cours.
   Relations : Utilisateur, Etudiant, Inscription
   --> 3 relations
========================================================= */
SELECT 'REQU�TE 9 : Afficher les �tudiants qui ont r�ussi tous leurs cours. Relations : Utilisateur, Etudiant, Inscription.' AS Info;

SELECT 
    u.id_User,
    u.nom_User,
    u.prenom_User
FROM Utilisateur u
INNER JOIN Etudiant e
    ON u.id_User = e.id_User
INNER JOIN Inscription i
    ON e.id_User = i.id_Etud
GROUP BY u.id_User, u.nom_User, u.prenom_User
HAVING MIN(CASE WHEN i.decisionFi_Inscript = 'R�ussi' THEN 1 ELSE 0 END) = 1;
GO


/* =========================================================
   REQU�TE 10
   Afficher le nombre d'�tudiants inscrits par cours offert.
   Relations : CoursOffert, Cours, Inscription
   --> 3 relations
========================================================= */
SELECT 'REQU�TE 10 : Afficher le nombre d''�tudiants inscrits par cours offert. Relations : CoursOffert, Cours, Inscription.' AS Info;

SELECT 
    co.id_CoursOf,
    c.nom_Cours,
    COUNT(i.id_Inscript) AS nb_Etudiants_Inscrits
FROM CoursOffert co
INNER JOIN Cours c
    ON co.id_Cours = c.id_Cours
LEFT JOIN Inscription i
    ON co.id_CoursOf = i.id_CoursOf
GROUP BY co.id_CoursOf, c.nom_Cours
ORDER BY nb_Etudiants_Inscrits DESC;
GO