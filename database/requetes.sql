

-- REQUÊTES COMPLEXES (au moins 4 relations)

-- Afficher les professeurs avec les cours qu'ils enseignent
SELECT 
    p.id_User AS id_Professeur,
    u.nom_User AS nom,
    u.prenom_User AS prenom,
    p.grade_Prof,
    p.statut_Prof,
    c.code_Cours,
    c.nom_Cours,
    co.sect_CoursOf,
    co.horaire_CoursOf
FROM Professeur p
JOIN Utilisateur u ON p.id_User = u.id_User
JOIN Enseigner e ON p.id_User = e.id_Prof
JOIN CoursOffert co ON e.id_CoursOf = co.id_CoursOf
JOIN Cours c ON co.id_Cours = c.id_Cours
ORDER BY u.nom_User, c.nom_Cours;


-- Afficher les étudiants avec leur programme et les cours auxquels ils sont inscrits
SELECT 
    e.id_User AS id_Etudiant,
    u.nom_User AS nom,
    u.prenom_User AS prenom,
    p.nom_Prog AS programme,
    c.code_Cours,
    c.nom_Cours,
    i.statut_Inscript,
    i.noteFi_Inscript,
    i.noteLet_Inscript
FROM Etudiant e
JOIN Utilisateur u ON e.id_User = u.id_User
JOIN Programme p ON e.programme_Etud = p.id_Prog
JOIN Inscription i ON e.id_User = i.id_Etud
JOIN CoursOffert co ON i.id_CoursOf = co.id_CoursOf
JOIN Cours c ON co.id_Cours = c.id_Cours
ORDER BY u.nom_User, c.nom_Cours;


-- Afficher les évaluations avec le cours offert, la session d'examen et le semestre
SELECT 
    ev.id_Eval,
    ev.nom_Eval,
    ev.type_Eval,
    ev.date_Eval,
    ev.poids_Eval,
    c.nom_Cours,
    se.type_SessExam,
    s.nom_Semest,
    s.annee_Semest
FROM Evaluation ev
JOIN CoursOffert co ON ev.id_CoursOf = co.id_CoursOf
JOIN Cours c ON co.id_Cours = c.id_Cours
JOIN SessionExamen se ON ev.id_SessExam = se.id_SessExam
JOIN Semestre s ON se.id_Semest = s.id_Semest
ORDER BY s.annee_Semest, s.nom_Semest, c.nom_Cours;


-- Afficher les notes des étudiants pour chaque évaluation et chaque cours
SELECT 
    u.nom_User AS nom,
    u.prenom_User AS prenom,
    c.nom_Cours,
    ev.nom_Eval,
    ev.type_Eval,
    n.valNum_Note,
    n.ValLettre_Note,
    n.dateAttrib_Note
FROM Note n
JOIN Inscription i ON n.id_Inscript = i.id_Inscript
JOIN Etudiant e ON i.id_Etud = e.id_User
JOIN Utilisateur u ON e.id_User = u.id_User
JOIN Evaluation ev ON n.id_Eval = ev.id_Eval
JOIN CoursOffert co ON ev.id_CoursOf = co.id_CoursOf
JOIN Cours c ON co.id_Cours = c.id_Cours
ORDER BY u.nom_User, c.nom_Cours, ev.date_Eval;


-- Afficher les choix de spécialisation des étudiants avec leur programme
SELECT 
    u.nom_User AS nom,
    u.prenom_User AS prenom,
    cs.date_ChoixSpec,
    cs.nb_ChoixSpec,
    sp.nom_Spec AS specialisation,
    p.nom_Prog AS programme
FROM ChoixSpecialisation cs
JOIN Etudiant e ON cs.id_Etud = e.id_User
JOIN Utilisateur u ON e.id_User = u.id_User
JOIN Specialisation sp ON cs.id_Spec = sp.id_Spec
JOIN Programme p ON e.programme_Etud = p.id_Prog
ORDER BY u.nom_User, cs.nb_ChoixSpec;


-- REQUÊTES SIMPLES

-- Afficher tous les semestres
SELECT *
FROM Semestre;


-- Afficher les semestres d'hiver
SELECT *
FROM Semestre
WHERE nom_Semest = 'Hiver';


-- Compter le nombre total de professeurs
SELECT COUNT(*) AS NombreTotalProfesseurs
FROM Professeur;


-- Afficher les professeurs avec leur nom et prénom
SELECT 
    p.id_User,
    u.nom_User,
    u.prenom_User,
    p.grade_Prof,
    p.statut_Prof
FROM Professeur p
JOIN Utilisateur u ON p.id_User = u.id_User
ORDER BY u.nom_User;


-- Compter le nombre d'étudiants par programme
SELECT 
    p.nom_Prog,
    COUNT(e.id_User) AS NombreEtudiants
FROM Etudiant e
JOIN Programme p ON e.programme_Etud = p.id_Prog
GROUP BY p.nom_Prog
ORDER BY NombreEtudiants DESC;