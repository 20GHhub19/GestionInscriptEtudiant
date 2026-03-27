USE GestionInscriptEtudiant;
GO

/* ###########################################################
   1) Suppression des données dans le bon ordre (FK respectées)
########################################################### */
DELETE FROM Note;
DELETE FROM Inscription;
DELETE FROM ChoixSpecialisation;
DELETE FROM Evaluation;
DELETE FROM Enseigner;
DELETE FROM SessionExamen;
DELETE FROM CoursOffert;
DELETE FROM CoursPrerequis;
DELETE FROM CoursProgramme;
DELETE FROM Restreindre;
DELETE FROM Professeur;
DELETE FROM Etudiant;
DELETE FROM Administrateur;
DELETE FROM Utilisateur;
DELETE FROM Specialisation;
DELETE FROM Cours;
DELETE FROM Semestre;
DELETE FROM Programme;

/* ###########################################################
   2) Réinitialisation des colonnes IDENTITY (RESEED)
   Permet de recommencer les identifiants depuis le début
########################################################### */

DBCC CHECKIDENT ('Programme', RESEED, 999);
DBCC CHECKIDENT ('Specialisation', RESEED, 9);
DBCC CHECKIDENT ('Utilisateur', RESEED, -97);
DBCC CHECKIDENT ('Cours', RESEED, 0);
DBCC CHECKIDENT ('CoursProgramme', RESEED, 0);
DBCC CHECKIDENT ('CoursPrerequis', RESEED, 0);
DBCC CHECKIDENT ('CoursOffert', RESEED, 0);
DBCC CHECKIDENT ('Semestre', RESEED, 0);
DBCC CHECKIDENT ('SessionExamen', RESEED, 0);
DBCC CHECKIDENT ('Evaluation', RESEED, 0);
DBCC CHECKIDENT ('ChoixSpecialisation', RESEED, 0);
DBCC CHECKIDENT ('Inscription', RESEED, 9);
DBCC CHECKIDENT ('Note', RESEED, 9);
DBCC CHECKIDENT ('Restreindre', RESEED, 0);
DBCC CHECKIDENT ('Enseigner', RESEED, 0);

--- ####################################### Insertion dans les tables ##############################################

-- 1) Insertion dans la table Programme x 10


INSERT INTO Programme(code_Prog, nom_Prog, descript_Prog, nbCredit_Prog, unite_Prog, duree_Prog)
VALUES
	('INF01','Informatique','Programme informatique',90,'Departement TI',3),
	('NET01','Reseaux','Programme reseaux',60,'Departement TI',2),
	('LOG01','Logiciel','Genie logiciel',90,'Departement TI',3),
	('SEC01','Cybersécurité','Sécurité informatique',75,'Departement TI',3),
	('DAT01','Science Données','Analyse des données',90,'Departement TI',3),
	('AI001','Intelligence Artificielle','IA',90,'Departement TI',3),
	('WEB01','Développement Web','Web full-stack',60,'Departement TI',2),
	('MOB01','Mobile','Applications mobiles',60,'Departement TI',2),
	('SYS01','Systèmes','Administration systèmes',60,'Departement TI',2),
	('GAM01','Jeux vidéo','Développement jeux',90,'Departement TI',3);


-- 2) Insertion dans la table Specialisation x 10
DECLARE @id_Spec INT
INSERT INTO Specialisation(nom_Spec, descript_Spec, nbCredit_Spec, id_Prog_Spec)
VALUES
	('IA','Intelligence artificielle',30,1005),
	('Sécurité réseau','Protection réseaux',30,1003),
	('Cloud','Informatique nuagique',30,1008),
	('DevOps','Integration continue',30,1002),
	('Data Mining','Fouille données',30,1004),
	('Web avancé','Technologies Web',30,1006),
	('Mobile avancé','Apps mobiles',30,1007),
	('Jeux 3D','Graphique temps réel',30,1009),
	('Administration','Systèmes serveurs',30,1008),
	('Cybersécurité','Sécurité offensive',30,1003);

SET @id_Spec = SCOPE_IDENTITY();


-- 3) Insertion dans la table Utilisateur x 20
DECLARE @sql_User NVARCHAR(MAX) = '';

SELECT @sql_User += 
'ALTER TABLE Utilisateur DROP CONSTRAINT ' + name + ';'
FROM sys.check_constraints
WHERE parent_object_id = OBJECT_ID('Utilisateur');

EXEC(@sql_User);

INSERT INTO Utilisateur(nom_User, prenom_User, dateNais_User, numTel_User, courriel_User, adresse_User ) 
VALUES 
	('Dupont','Jean','2004-05-12','(514)-123-4567','jean.dupont@yahoo.com','Montréal'),
	('Heumen','Gaius','1999-08-08','(581)-123-4567','gaius@gmail.com','Montréal'),
	('Martin','Paul','2003-01-10','(438)-111-2222','paul.martin@gmail.com','Laval'),
	('Nguyen','Lan','2002-07-21','(514)-333-4444','lan.nguyen@gmail.com','Montréal'),
	('Roy','Sophie','2001-12-30','(450)-555-6666','sophie.roy@gmail.com','Longueuil'),
	('Smith','John','2000-03-15','(819)-777-8888','john.smith@gmail.com','Gatineau'),
	('Diallo','Aminata','2004-09-09','(514)-999-0000','aminata@gmail.com','Montréal'),
	('Chen','Li','2003-11-11','(438)-222-3333','li.chen@gmail.com','Montréal'),
	('Garcia','Luis','2002-06-06','(450)-444-5555','luis@gmail.com','Brossard'),
	('Tremblay','Marc','2001-02-02','(418)-666-7777','marc@gmail.com','Québec'),
	('Durand','Pierre','1975-05-05','(514)-111-1111','p.durand@gmail.com','Montréal'),
	('Lefevre','Claire','1980-02-02','(514)-222-2222','c.lefevre@gmail.com','Montréal'),
	('Smith','Robert','1970-03-03','(819)-333-3333','r.smith@gmail.com','Gatineau'),
	('Khan','Ali','1985-04-04','(450)-444-4444','ali.khan@gmail.com','Laval'),
	('Dubois','Marie','1978-06-06','(418)-555-5555','m.dubois@gmail.com','Québec'),
	('Nguyen','Minh','1982-07-07','(514)-666-6666','minh@gmail.com','Montréal'),
	('Roy','Luc','1969-08-08','(514)-777-7777','luc.roy@gmail.com','Montréal'),
	('Garcia','Ana','1983-09-09','(438)-888-8888','ana@gmail.com','Montréal'),
	('Chen','Wei','1977-10-10','(514)-999-9999','wei@gmail.com','Montréal'),
	('Diallo','Moussa','1981-11-11','(514)-000-0000','moussa@gmail.com','Montréal');


-- 4) Insertion dans la table Administrateur x 2
INSERT INTO Administrateur (id_User, role_Admin_Etud)
VALUES
	(1803, 'Gestion des inscriptions'),
	(1903, 'Gestion académique');


-- 5) Insertion dans la table Etudiant x 10
INSERT INTO Etudiant (id_User, statut_Etud, programme_Etud)
VALUES
	(3,    'Actif', 1001),
	(103,  'Actif', 1000),
	(203,  'Actif', 1002),
	(303,  'Actif', 1005),
	(403,  'Actif', 1003),
	(503,  'Actif', 1004),
	(603,  'Actif', 1006),
	(703,  'Actif', 1007),
	(803,  'Actif', 1008),
	(903,  'Actif', 1009);


-- 6) Insertion dans la table Cours x 10
INSERT INTO Cours(code_Cours, nom_Cours, credit_Cours, type_Cours, nbHeure_Cours)
VALUES
	('INF01','Programmation',3,'Théorique',45),
	('INF02','Structures données',3,'Théorique',45),
	('INF03','Bases données',3,'Théorique',45),
	('INF04','Réseaux',3,'Laboratoire',60),
	('INF05','Systèmes',3,'Théorique',45),
	('INF06','IA',3,'Théorique',45),
	('INF07','Web',3,'Travaux Pratiques',60),
	('INF08','Mobile',3,'Travaux Pratiques',60),
	('INF09','Cybersécurité',3,'Théorique',45),
	('INF10','Cloud',3,'Théorique',45);


-- 7) Insertion dans la table CoursProgramme x 10
INSERT INTO CoursProgramme(id_Prog, id_Cours)
VALUES
	(1000,1),(1000,2),(1001,4),(1002,3),(1003,9),
	(1004,3),(1005,6),(1006,7),(1007,8),(1008,10);


-- 8) Insertion dans la table CoursPrerequis x 10
INSERT INTO CoursPrerequis(id_Cours, id_Prerequis)
VALUES
	(2,1),(3,1),(4,2),(5,2),(6,3),
	(7,1),(8,7),(9,4),(10,5),(6,2);


-- 9) Insertion dans la table CoursOffert x 10
INSERT INTO CoursOffert(capacite_CoursOf, horaire_CoursOf, salle_CoursOf, dateDeb_CoursOf, dateFin_CoursOf, id_Cours)
VALUES
	(40,'Lun 09H-12H','A101','2024-01-10','2024-04-20',1),
	(35,'Mar 09H-12H','A102','2024-01-10','2024-04-20',2),
	(30,'Mer 09H-12H','A103','2024-01-10','2024-04-20',3),
	(25,'Jeu 09-12','A104','2024-01-10','2024-04-20',4),
	(40,'Ven 09H-12H','A105','2024-01-10','2024-04-20',5),
	(30,'Lun 13H-16H','B101','2024-01-10','2024-04-20',6),
	(30,'Mar 13H-16H','B102','2024-01-10','2024-04-20',7),
	(25,'Mer 13H-16H','B103','2024-01-10','2024-04-20',8),
	(30,'Jeu 13H-16H','B104','2024-01-10','2024-04-20',9),
	(20,'Ven 13H-16H','B105','2024-01-10','2024-04-20',10);


-- 10) Insertion dans la table Semestre x 10
INSERT INTO Semestre(nom_Semest, annee_Semest, datDeb_Semest, dateFin_Semest)
VALUES
	('Hiver','2024','2024-01-10','2024-04-20'),
	('Été','2024','2024-05-10','2024-08-20'),
	('Automne','2024','2024-09-01','2024-12-20'),
	('Hiver','2025','2025-01-10','2025-04-20'),
	('Été','2025','2025-05-10','2025-08-20'),
	('Automne','2025','2025-09-01','2025-12-20'),
	('Hiver','2026','2026-01-10','2026-04-20'),
	('Été','2026','2026-05-10','2026-08-20'),
	('Automne','2026','2026-09-01','2026-12-20'),
	('Hiver','2027','2027-01-10','2027-04-20');


-- 11) Insertion dans la table SessionExamen x 10
INSERT INTO SessionExamen (type_SessExam, dateDeb, dateFin, id_Semest)
VALUES
	('Intra', '2024-02-15', '2024-02-20', 1),
	('Final', '2024-04-10', '2024-04-20', 1),
	('Intra', '2024-06-15', '2024-06-20', 2),
	('Final', '2024-08-10', '2024-08-20', 2),
	('Intra', '2024-10-15', '2024-10-20', 3),
	('Final', '2024-12-10', '2024-12-20', 3),
	('Intra', '2025-02-15', '2025-02-20', 4),
	('Final', '2025-04-10', '2025-04-20', 4),
	('Intra', '2025-06-15', '2025-06-20', 5),
	('Final', '2025-08-10', '2025-08-20', 5);


-- 12) Insertion dans la table Evaluation x 10
INSERT INTO Evaluation (type_Eval, date_Eval, nom_Eval, poids_Eval, descript_Eval, id_CoursOf, id_SessExam)
VALUES
	('Intra',               '2024-02-16', 'Intra Programmation',        30.00, 'Examen intra du cours Programmation',      1, 1),
	('Examen final',        '2024-04-15', 'Final Structures données',   40.00, 'Examen final du cours Structures données', 2, 2),
	('Travaux Pratiques 1', '2024-06-16', 'TP Bases données',           20.00, 'Travail pratique sur SQL',                3, 3),
	('Examen final',        '2024-08-15', 'Final Réseaux',              40.00, 'Évaluation finale en laboratoire',       4, 4),
	('Quiz',                '2024-10-16', 'Quiz Systèmes',              15.00, 'Quiz sur les systèmes d’exploitation',   5, 5),
	('Examen final',        '2024-12-15', 'Final IA',                   35.00, 'Examen final du cours IA',               6, 6),
	('Travaux Pratiques 1', '2025-02-16', 'TP Web',                     25.00, 'Projet pratique développement Web',      7, 7),
	('Travaux Pratiques 1', '2025-04-15', 'TP Mobile',                  25.00, 'Projet pratique mobile',                 8, 8),
	('Intra',               '2025-06-16', 'Intra Cybersécurité',        30.00, 'Examen intra cybersécurité',             9, 9),
	('Examen final',        '2025-08-15', 'Final Cloud',                40.00, 'Examen final cloud computing',           10, 10);


-- 13) Insertion dans la table Professeur x 8
INSERT INTO Professeur (id_User, grade_Prof, statut_Prof)
VALUES
	(1003, 'Chargé de cours',      'Permanent'),
	(1103, 'Professeur agrégé',    'temps plein'),
	(1203, 'Maître de conférence', 'Permanent'),
	(1303, 'Chargé de cours',      'contractuel'),
	(1403, 'Professeur titulaire', 'Permanent'),
	(1503, 'Auxilliaire de cours', 'temps partiel'),
	(1603, 'Professeur agrégé',    'Invité'),
	(1703, 'Chargé de cours',      'temps plein');


-- 14) Insertion dans la table ChoixSpecialisation x 10
INSERT INTO ChoixSpecialisation (date_ChoixSpec, nb_ChoixSpec, id_Etud, id_Spec)
VALUES
	('2024-01-15', 1, 3,   10),
	('2024-01-16', 1, 103, 11),
	('2024-01-17', 1, 203, 12),
	('2024-01-18', 1, 303, 13),
	('2024-01-19', 1, 403, 14),
	('2024-01-20', 1, 503, 15),
	('2024-01-21', 1, 603, 16),
	('2024-01-22', 1, 703, 17),
	('2024-01-23', 1, 803, 18),
	('2024-01-24', 1, 903, 19);


-- 15) Insertion dans la table Inscription x 10
INSERT INTO Inscription (
	statut_Inscript,
	date_Inscript,
	date_Desinscript,
	noteFi_Inscript,
	noteLet_Inscript,
	decisionFi_Inscript,
	tentative_Inscript,
	estValidePrerequis_Inscript,
	id_Etud,
	id_CoursOf
)
VALUES
	('Inscrit', '2024-01-08', NULL, 85.50, 'A', 'Réussi', 1, 1, 3,   1),
	('Inscrit', '2024-01-08', NULL, 78.00, 'B', 'Réussi', 1, 1, 103, 2),
	('Inscrit', '2024-01-08', NULL, 88.00, 'A', 'Réussi', 1, 1, 203, 3),
	('Inscrit', '2024-01-08', NULL, 69.50, 'C', 'Réussi', 1, 1, 303, 4),
	('Inscrit', '2024-01-08', NULL, 74.00, 'B', 'Réussi', 1, 1, 403, 5),
	('Inscrit', '2024-01-08', NULL, 91.00, 'A', 'Réussi', 1, 1, 503, 6),
	('Inscrit', '2024-01-08', NULL, 80.00, 'B', 'Réussi', 1, 1, 603, 7),
	('Inscrit', '2024-01-08', NULL, 77.00, 'B', 'Réussi', 1, 1, 703, 8),
	('Inscrit', '2024-01-08', NULL, 66.00, 'C', 'Réussi', 1, 1, 803, 9),
	('Inscrit', '2024-01-08', NULL, 59.00, 'D', 'Échec',  1, 1, 903, 10);


-- 16) Insertion dans la table Note x 10
INSERT INTO Note (valNum_Note, ValLettre_Note, retroAction, dateAttrib_Note, id_Inscript, id_Eval)
VALUES
	(85.50, 'A', 'Très bon travail',                  '2024-02-20', 10, 1),
	(78.00, 'B', 'Bonne maîtrise générale',           '2024-04-18', 11, 2),
	(88.00, 'A', 'Excellent en SQL',                  '2024-06-20', 12, 3),
	(69.50, 'C', 'Doit améliorer la partie pratique', '2024-08-18', 13, 4),
	(74.00, 'B', 'Résultat satisfaisant',             '2024-10-18', 14, 5),
	(91.00, 'A', 'Excellent rendement',               '2024-12-18', 15, 6),
	(80.00, 'B', 'Bon projet Web',                    '2025-02-20', 16, 7),
	(77.00, 'B', 'Application mobile correcte',       '2025-04-18', 17, 8),
	(66.00, 'C', 'Besoin de renforcer la sécurité',   '2025-06-20', 18, 9),
	(59.00, 'D', 'Échec, reprise recommandée',        '2025-08-18', 19, 10);


-- 17) Insertion dans la table Restreindre x 10
INSERT INTO Restreindre(nb_Restrict, id_Spec, id_Cours)
VALUES
	(2,@id_Spec,6),(3,@id_Spec,4),(1,@id_Spec,10),(2,@id_Spec,5),(1,@id_Spec,3),
	(2,@id_Spec,7),(1,@id_Spec,8),(3,@id_Spec,1),(2,@id_Spec,5),(1,@id_Spec,9);


-- 18) Insertion dans la table Enseigner x 10
INSERT INTO Enseigner (nbH_Enseigner, datDeb_Enseigner, dateFin_Enseigner, id_Prof, id_CoursOf)
VALUES
	(45, '2024-01-10', '2024-04-20', 1003, 1),
	(45, '2024-01-10', '2024-04-20', 1103, 2),
	(45, '2024-01-10', '2024-04-20', 1203, 3),
	(60, '2024-01-10', '2024-04-20', 1303, 4),
	(45, '2024-01-10', '2024-04-20', 1403, 5),
	(45, '2024-01-10', '2024-04-20', 1503, 6),
	(60, '2024-01-10', '2024-04-20', 1603, 7),
	(60, '2024-01-10', '2024-04-20', 1703, 8),
	(45, '2024-01-10', '2024-04-20', 1003, 9),
	(45, '2024-01-10', '2024-04-20', 1103, 10);