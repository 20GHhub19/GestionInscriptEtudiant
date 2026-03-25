-- REQUÊTES SUR LA TABLE Semestre


-- Afficher tous les semestres
SELECT * 
FROM Semestre;


-- Afficher les semestres de type "Hiver"
SELECT * 
FROM Semestre
WHERE nom_Semest = 'Hiver';


-- Afficher les semestres de l'année 2025
SELECT * 
FROM Semestre
WHERE annee_Semest = '2025';


-- Trier les semestres par date de début
SELECT * 
FROM Semestre
ORDER BY datDeb_Semest ASC;


-- Compter le nombre total de semestres
SELECT COUNT(*) AS NombreTotalSemestres
FROM Semestre;



-- REQUÊTES SUR LA TABLE Professeur

-- Afficher tous les professeurs
SELECT * 
FROM Professeur;


-- Afficher les professeurs avec leur nom et prénom (JOIN avec Utilisateur)
SELECT 
    p.id_User,
    u.nom_User,
    u.prenom_User,
    p.grade_Prof,
    p.statut_Prof
FROM Professeur p
JOIN Utilisateur u ON p.id_User = u.id_User;


-- Afficher les professeurs ayant le statut "Permanent"
SELECT 
    p.id_User,
    u.nom_User,
    u.prenom_User,
    p.grade_Prof,
    p.statut_Prof
FROM Professeur p
JOIN Utilisateur u ON p.id_User = u.id_User
WHERE p.statut_Prof = 'Permanent';


-- Trier les professeurs par nom de famille (ordre alphabétique)
SELECT 
    p.id_User,
    u.nom_User,
    u.prenom_User,
    p.grade_Prof,
    p.statut_Prof
FROM Professeur p
JOIN Utilisateur u ON p.id_User = u.id_User
ORDER BY u.nom_User ASC;


-- Compter le nombre total de professeurs
SELECT COUNT(*) AS NombreTotalProfesseurs
FROM Professeur;