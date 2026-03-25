/*
 I- a) ########################################################### Création de la base de donnees GestionInscriptEtudiant ###############################################################################
*/

-- On utilise la base de données incluse de base avec SQLserver.
-- Si on ne fait pas cela, on ne pourra pas supprimer GestionEtudiant puisqu'on
-- serait en train de l'utiliser.
USE master;
GO

-- On supprime la base de données juste si elle existe.
-- Cela évite une erreur si on a jamais exécuté le script.
--DROP DATABASE IF EXISTS GestionInscriptEtudiant;
--IF NOT USE DATABASE 

-- Création de la base de données si elle n'existe pas
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GestionInscriptEtudiant')
BEGIN
	-- On crée la base de données Gestion des notes et inscriptions des étudiants
	CREATE DATABASE GestionInscriptEtudiant;
END;
GO

-- On utilise la base de données GestionInscriptEtudiant pour ajouter
-- des tables, triggers, etc.
USE GestionInscriptEtudiant;
GO

--- #######################  Création des tables ###########################

-- 1-) Table Programme

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Specialisation_Programme' 	)
	BEGIN
		ALTER TABLE Specialisation
		DROP CONSTRAINT FK_Specialisation_Programme;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Etudiant_Programme' OR NAME = 'FK_Etudiant_Utilisateur'
	)
	BEGIN
		ALTER TABLE Etudiant
		DROP CONSTRAINT FK_Etudiant_Programme, FK_Etudiant_Utilisateur
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_CoursProgramme_Programme' OR name = 'FK_CoursProgramme_Cours'
	)
	BEGIN
		ALTER TABLE CoursProgramme
		DROP CONSTRAINT FK_CoursProgramme_Programme, FK_CoursProgramme_Cours
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_CoursPrerequis_Cours' OR NAME = 'FK_CoursPrerequis_Prerequis'
	)
	BEGIN
		ALTER TABLE CoursPrerequis
		DROP CONSTRAINT FK_CoursPrerequis_Cours, FK_CoursPrerequis_Prerequis;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_CoursOffert_Cours'
	)
	BEGIN
		ALTER TABLE CoursOffert
		DROP CONSTRAINT FK_CoursOffert_Cours;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_SessionExamen_Semestre'
	)
	BEGIN
		ALTER TABLE SessionExamen
		DROP CONSTRAINT FK_SessionExamen_Semestre;
	END
	
IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Evaluation_CoursOffert' OR NAME = 'FK_Evaluation_SessionExamen'
	)
	BEGIN
		ALTER TABLE Evaluation
		DROP CONSTRAINT FK_Evaluation_CoursOffert, FK_Evaluation_SessionExamen;
	END
	
IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_ChoixSpecialisation_Etudiant' OR NAME = 'FK_ChoixSpecialisation_Specialisation'
	)
	BEGIN
		ALTER TABLE ChoixSpecialisation
		DROP CONSTRAINT FK_ChoixSpecialisation_Etudiant, FK_ChoixSpecialisation_Specialisation;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Inscription_Etudiant' OR NAME = 'FK_Inscription_CoursOffert'
	)
	BEGIN
		ALTER TABLE Inscription
		DROP CONSTRAINT FK_Inscription_Etudiant, FK_Inscription_CoursOffert;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Note_Inscription' OR NAME = 'FK_Note_Evaluation'
	)
	BEGIN
		ALTER TABLE Note
		DROP CONSTRAINT FK_Note_Inscription, FK_Note_Evaluation;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Restreindre_Specialisation' OR NAME = 'FK_Restreindre_Cours'
	)
	BEGIN
		ALTER TABLE Restreindre
		DROP CONSTRAINT FK_Restreindre_Specialisation, FK_Restreindre_Cours;
	END


IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Enseigner_Professeur'
	)
	BEGIN
		ALTER TABLE Enseigner
		DROP CONSTRAINT FK_Enseigner_Professeur, FK_Enseigner_CoursOffert;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Professeur_Utilisateur'
	)
	BEGIN
		ALTER TABLE Professeur
		DROP CONSTRAINT FK_Professeur_Utilisateur;
	END

IF EXISTS (
	SELECT 1
	FROM sys.foreign_keys
	WHERE NAME = 'FK_Administrateur_Utilisateur'
	)
	BEGIN
		ALTER TABLE Administrateur
		DROP CONSTRAINT FK_Administrateur_Utilisateur;
	END




DROP TABLE IF EXISTS Note;
DROP TABLE IF EXISTS Inscription;
DROP TABLE IF EXISTS ChoixSpecialisation;
DROP TABLE IF EXISTS Evaluation;
DROP TABLE IF EXISTS SessionExamen;
DROP TABLE IF EXISTS CoursOffert;
DROP TABLE IF EXISTS CoursPrerequis;
DROP TABLE IF EXISTS CoursProgramme;
DROP TABLE IF EXISTS Restreindre;
DROP TABLE IF EXISTS Enseigner;
DROP TABLE IF EXISTS Utilisateur;
DROP TABLE IF EXISTS Administrateur;
DROP TABLE IF EXISTS Etudiant;
DROP TABLE IF EXISTS Specialisation;
DROP TABLE IF EXISTS Professeur;
DROP TABLE IF EXISTS Semestre;
DROP TABLE IF EXISTS Cours;
DROP TABLE IF EXISTS Programme;


CREATE TABLE Programme (
	id_Prog INT IDENTITY(1000, 1) NOT NULL,
	code_Prog VARCHAR(5) NOT NULL,
	nom_Prog VARCHAR(30) NOT NULL,
	descript_Prog VARCHAR(400),
	nbCredit_Prog INT NOT NULL,
	unite_Prog VARCHAR(30),
	duree_Prog INT,
	dateCreat_Prog DATE CONSTRAINT DF_Prog_dateCreat_Prog DEFAULT CAST(GETDATE() AS  DATE),
	CONSTRAINT PK_Programme PRIMARY KEY(id_Prog)
);

-- 2-) Table Spécialisation

CREATE TABLE Specialisation (
	id_Spec INT IDENTITY(10, 1) NOT NULL,
	nom_Spec VARCHAR (30) NOT NULL,
	descript_Spec VARCHAR(400),
	nbCredit_Spec INT NOT NULL,
	id_Prog_Spec INT,
	CONSTRAINT PK_Specialisation PRIMARY KEY (id_Spec),
	CONSTRAINT FK_Specialisation_Programme FOREIGN KEY (id_Prog_Spec) REFERENCES Programme(id_Prog)
)
CREATE TABLE Utilisateur (
	id_User INT  IDENTITY (100, 1),
	nom_User VARCHAR(15) NOT NULL,
	prenom_User VARCHAR(20) NOT NULL,
	dateInscriptUser DATE NOT NULL DEFAULT GETDATE(),
	mat_User AS (CAST(YEAR(dateInscriptUser) AS VARCHAR(4)) +
	RIGHT('00' + CAST(MONTH(dateInscriptUser) AS VARCHAR(2)), 2) +
	RIGHT('0000' + CAST(id_User AS VARCHAR(4)), 4)) PERSISTED UNIQUE,
	dateNais_User Date NOT NULL,
	numTel_User VARCHAR(15),
	CHECK(numTel_User IS NULL OR numTel_User LIKE '[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	courriel_User VARCHAR(30),
	CHECK(courriel_User LIKE '%_@_%._%'), 
	adresse_User  VARCHAR(80),
	CONSTRAINT PK_Utilisateur PRIMARY KEY(id_User)
)

CREATE TABLE Administrateur (
	id_User INT NOT NULL,
	role_Admin_Etud VARCHAR(30) NOT NULL,
	CONSTRAINT PK_Administarteur PRIMARY KEY (id_User),
	CONSTRAINT FK_Administrateur_Utilisateur FOREIGN KEY(id_User) REFERENCES Utilisateur(id_User),
)

-- 3-) Table Etudiant
CREATE TABLE Etudiant (
	id_User INT NOT NULL,
	statut_Etud VARCHAR(30) NOT NULL,
	programme_Etud INT NOT NULL,
	CONSTRAINT PK_Etudiant PRIMARY KEY (id_User),
	CONSTRAINT FK_Etudiant_Programme FOREIGN KEY(programme_Etud) REFERENCES Programme(id_Prog),
	CONSTRAINT FK_Etudiant_Utilisateur FOREIGN KEY(id_User) REFERENCES Utilisateur(id_User)
)

-- 4 Création de la table Cours 

CREATE TABLE Cours (
	id_Cours INT IDENTITY(1, 1) NOT NULL,
	code_Cours VARCHAR(5) UNIQUE NOT NULL,
	nom_Cours VARCHAR(30) NOT NULL,
	credit_Cours INT NOT NULL,
	type_Cours VARCHAR (30) NOT NULL DEFAULT 'Théorique',
	nbHeure_Cours INT,
	CONSTRAINT CK_Cours_type_Cours CHECK (type_Cours IN ('Théorique', 'Laboratoire', 'Démo', 'Travaux Pratiques')),
	CONSTRAINT PK_Cours PRIMARY KEY(id_Cours)
)

-- 5 Création de la table CoursProgramme

CREATE TABLE CoursProgramme (
	id_CoursProg INT IDENTITY(1, 1),
	id_Prog INT NOT NULL,
	id_Cours INT NOT NULL,
	CONSTRAINT PK_CoursProgramme PRIMARY KEY(id_CoursProg),
	CONSTRAINT FK_CoursProgramme_Programme FOREIGN KEY(id_Prog) REFERENCES Programme(id_Prog),
	CONSTRAINT FK_CoursProgramme_Cours FOREIGN KEY (id_Cours) REFERENCES Cours(id_Cours)
)

-- 6- Création de la table CoursPrerequis

CREATE TABLE CoursPrerequis (
	id_CoursPre INT IDENTITY(1, 1),
	id_Cours INT NOT NULL,
	id_Prerequis INT NOT NULL,
	CONSTRAINT PK_CoursPrerequis PRIMARY KEY (id_CoursPre),
	CONSTRAINT FK_CoursPrerequis_Cours FOREIGN KEY(id_Cours) REFERENCES Cours(id_Cours),
	CONSTRAINT FK_CoursPrerequis_Prerequis FOREIGN KEY(id_Prerequis) REFERENCES Cours(id_Cours)
)

-- 7- Création de la table CoursOffert

CREATE TABLE CoursOffert (
	id_CoursOf INT IDENTITY(1, 1),
	sect_CoursOf VARCHAR(3) DEFAULT 'A',
	capacite_CoursOf INT NOT NULL,
	horaire_CoursOf VARCHAR(20) NOT NULL,
	salle_CoursOf VARCHAR(15),
	dateDeb_CoursOf DATE,
	dateFin_CoursOf DATE,
	mondeEns_CoursOf VARCHAR(30) DEFAULT 'Présentiel',
	id_Cours INT NOT NULL,
	CONSTRAINT CK_CoursOf_mondeEns_CoursOf CHECK (mondeEns_CoursOf IN ('Présentiel', 'Asynchrone', 'En ligne', 'Bimodal')),
	CONSTRAINT PK_CoursOffert PRIMARY KEY(id_CoursOf),
	CONSTRAINT FK_CoursOffert_Cours FOREIGN KEY(id_Cours) REFERENCES Cours(id_Cours)
)

-- 8-) Création de la table Semestre

CREATE TABLE Semestre (
	id_Semest INT IDENTITY (1, 1),
	nom_Semest VARCHAR(20) NOT NULL,
	annee_Semest VARCHAR(20) NOT NULL,
	datDeb_Semest DATE NOT NULL,
	dateFin_Semest DATE,
	CONSTRAINT UQ_Semestre_nom_annee UNIQUE(nom_Semest, annee_Semest),
	CONSTRAINT PK_Semestre PRIMARY KEY(id_Semest)
)

-- 9) Création de la table SessionExamen

CREATE TABLE SessionExamen (
	id_SessExam INT IDENTITY(1, 1),
	type_SessExam VARCHAR(15) NOT NULL,
	dateDeb DATE NOT NULL,
	dateFin DATE,
	id_Semest INT NOT NULL,
	CONSTRAINT UQ_SessionExamen_type_dateDeb UNIQUE (type_SessExam, dateDeb),
	CONSTRAINT PK_SessionExamen PRIMARY KEY(id_SessExam),
	CONSTRAINT FK_SessionExamen_Semestre FOREIGN KEY(id_Semest) REFERENCES Semestre(id_Semest)
)

-- 10-) Création de la table Evaluation

CREATE TABLE Evaluation (
	id_Eval INT IDENTITY(1, 1),
	type_Eval VARCHAR(50) DEFAULT 'Travaux Pratiques',
	date_Eval DATE NOT NULL,
	nom_Eval VARCHAR(50) NOT NULL,
	poids_Eval DECIMAL(4, 2),
	descript_Eval VARCHAR(100),
	id_CoursOf INT NOT NULL,
	id_SessExam INT NOT NULL,
	--id_Semest INT NOT NULL,
	CONSTRAINT CK_Evaluation_type_Eval CHECK(type_Eval LIKE 'Travaux Pratiques%' OR
	type_Eval IN ('Intra', 'Quiz', 'Examen final')),
	CONSTRAINT PK_Evaluation PRIMARY KEY(id_Eval),
	CONSTRAINT FK_Evaluation_CoursOffert FOREIGN KEY(id_CoursOf) REFERENCES CoursOffert(id_CoursOf),
	CONSTRAINT FK_Evaluation_SessionExamen FOREIGN KEY(id_SessExam) REFERENCES SessionExamen(id_SessExam),
)

-- 11-) Création de la table Professeur

CREATE TABLE Professeur (
	id_User INT NOT NULL,
	grade_Prof VARCHAR(50) DEFAULT 'Chargé de cours',
	statut_Prof VARCHAR(30) DEFAULT 'Permanent',
	CONSTRAINT PK_Professeur PRIMARY KEY(id_User),
	CONSTRAINT FK_Professeur_Utilisateur FOREIGN KEY(id_User) REFERENCES Utilisateur(id_User),
	CONSTRAINT CK_Professeur_grade_Prof CHECK(grade_Prof IN('Professeur Emerite', 'Professeur agrégé', 'Professeur titulaire','Maître de conférence', 'Chargé de cours', 'Auxilliaire de cours')),
	CONSTRAINT CK_Professeur_statut_Prof CHECK(statut_Prof IN('Permanent', 'contractuel', 'temps plein', 'temps partiel', 'Invité', 'Retraité actif'))
)


-- 12-) Création de la table ChoixSpecialisation

CREATE TABLE ChoixSpecialisation (
	id_ChoixSpec INT IDENTITY(1, 1),
	date_ChoixSpec DATE NOT NULL,
	nb_ChoixSpec INT,
	id_Etud INT NOT NULL,
	id_Spec INT NOT NULL,
	CONSTRAINT PK_ChoixSpecialisation PRIMARY KEY(id_ChoixSpec),
	CONSTRAINT FK_ChoixSpecialisation_Etudiant FOREIGN KEY(id_Etud) REFERENCES Etudiant(id_User),
	CONSTRAINT FK_ChoixSpecialisation_Specialisation FOREIGN KEY(id_Spec) REFERENCES Specialisation(id_Spec)
)

-- 13-) Création de la table Inscription

CREATE TABLE Inscription (
	id_Inscript INT IDENTITY(10, 1),
	statut_Inscript VARCHAR(20),
	date_Inscript DATE DEFAULT GETDATE(),
	date_Desinscript DATE,
	noteFi_Inscript DECIMAL(4, 2),
	noteLet_Inscript VARCHAR(2),
	decisionFi_Inscript VARCHAR(20),
	tentative_Inscript INT DEFAULT 1,
	estValidePrerequis_Inscript BIT NOT NULL DEFAULT 0,
	id_Etud INT NOT NULL,
	id_CoursOf INT NOT NULL,
	CONSTRAINT PK_Inscription PRIMARY KEY(id_Inscript),
	CONSTRAINT FK_Inscription_Etudiant FOREIGN KEY(id_Etud) REFERENCES Etudiant(id_User),
	CONSTRAINT FK_Inscription_CoursOffert FOREIGN KEY(id_CoursOf) REFERENCES CoursOffert(id_CoursOf)
)
-- 14 -) Création de la table Note

CREATE TABLE Note (
	id_Note INT IDENTITY(10, 1),
	valNum_Note DECIMAL(4, 2) NOT NULL DEFAULT 00.00,
	ValLettre_Note VARCHAR(2) NOT NULL,
	retroAction VARCHAR(100),
	dateAttrib_Note DATE,
	id_Inscript INT NOT NULL,
	id_Eval INT NOT NULL,
	CONSTRAINT PK_Note PRIMARY KEY(id_Note),
	CONSTRAINT FK_Note_Inscription FOREIGN KEY(id_Inscript) REFERENCES Inscription(id_Inscript),
	CONSTRAINT FK_Note_Evaluation FOREIGN KEY(id_Eval) REFERENCES Evaluation(id_Eval)
)

-- 15) Création de la table Restreindre

CREATE TABLE Restreindre (
	id_Rest INT IDENTITY(1, 1),
	nb_Restrict INT,
	id_Spec INT NOT NULL,
	id_Cours INT NOT NULL,
	CONSTRAINT PK_Restreindre PRIMARY KEY(id_Rest),
	CONSTRAINT FK_Restreindre_Specialisation FOREIGN KEY(id_Spec)  REFERENCES Specialisation(id_Spec),
	CONSTRAINT FK_Restreindre_Cours FOREIGN KEY(id_Cours) REFERENCES Cours(id_Cours)
)

-- 16-) Création de la table Enseigner

CREATE TABLE Enseigner (
	id_Enseigner INT IDENTITY(1, 1),
	nbH_Enseigner INT NOT NULL,
	datDeb_Enseigner Date,
	dateFin_Enseigner Date,
	id_Prof INT NOT NULL,
	id_CoursOf INT NOT NULL,
	CONSTRAINT PK_Enseigner PRIMARY KEY(id_Enseigner),
	CONSTRAINT FK_Enseigner_Professeur FOREIGN KEY(id_Prof) REFERENCES Professeur(id_User),
	CONSTRAINT FK_Enseigner_CoursOffert FOREIGN KEY(id_CoursOf) REFERENCES CoursOffert(id_CoursOf)
	)

/*
Références 

1- Spécification des colonnes calculées
https://learn.microsoft.com/fr-fr/sql/relational-databases/tables/specify-computed-columns-in-a-table?view=sql-server-ver17
https://www.sqlservertutorial.net/sql-server-basics/sql-server-computed-columns/

2- Comment utiliser les pattern matching avec LIKE ET ESCAPE 
https://learn.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql?view=sql-server-ver17
https://www.sqlservertutorial.net/sql-server-basics/sql-server-like/
*/


--- ####################################### Insertion dans les tables ##############################################

-- 1 Insertion dans la table Programme x 10
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


-- 2- Insertion dans la table Specialisation x 10

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



INSERT INTO Utilisateur(nom_User, prenom_User, dateNais_User, numTel_User, courriel_User, adresse_User ) 
VALUES 
	('Dupont','Jean','2004-05-12','5141234567','jean.dupont@yahoo.com','Montréal'),
	('Heumen','Gaius','1999-08-08','5811234567','gaius@gmail.com','Montréal'),
	('Martin','Paul','2003-01-10','4381112222','paul.martin@gmail.com','Laval'),
	('Nguyen','Lan','2002-07-21','5143334444','lan.nguyen@gmail.com','Montréal'),
	('Roy','Sophie','2001-12-30','4505556666','sophie.roy@gmail.com','Longueuil'),
	('Smith','John','2000-03-15','8197778888','john.smith@gmail.com','Gatineau'),
	('Diallo','Aminata','2004-09-09','5149990000','aminata@gmail.com','Montréal'),
	('Chen','Li','2003-11-11','4382223333','li.chen@gmail.com','Montréal'),
	('Garcia','Luis','2002-06-06','4504445555','luis@gmail.com','Brossard'),
	('Tremblay','Marc','2001-02-02','4186667777','marc@gmail.com','Québec'),
	('Durand','Pierre','1975-05-05','5141111111','p.durand@gmail.com','Montréal'),
	('Lefevre','Claire','1980-02-02','5142222222','c.lefevre@gmail.com','Montréal'),
	('Smith','Robert','1970-03-03','8193333333','r.smith@gmail.com','Gatineau'),
	('Khan','Ali','1985-04-04','4504444444','ali.khan@gmail.com','Laval'),
	('Dubois','Marie','1978-06-06','4185555555','m.dubois@gmail.com','Québec'),
	('Nguyen','Minh','1982-07-07','5146666666','minh@gmail.com','Montréal'),
	('Roy','Luc','1969-08-08','5147777777','luc.roy@gmail.com','Montréal'),
	('Garcia','Ana','1983-09-09','4388888888','ana@gmail.com','Montréal'),
	('Chen','Wei','1977-10-10','5149999999','wei@gmail.com','Montréal'),
	('Diallo','Moussa','1981-11-11','5140000000','moussa@gmail.com','Montréal');



INSERT INTO Etudiant
(id_User, statut_Etud, programme_Etud)
VALUES
	(100, 'Inactif',1001), (102, 'Actif',1000), (105, 'Actif',1002), (115, 'Actif',1005),
	(109, 'Actif',1003), (103, 'Inactif',1004), (108, 'Inactif',1006),(112, 'Actif',1007),
	(117, 'Actif',1008), (110, 'Inactif',1009);

-- Insertion dans la table Administrateur
INSERT INTO Administrateur
	(id_User, role_Admin_Etud)
VALUES
	(104, 'Gestionnaire du site'),
	(108, 'Administrateur d''applications')
-- 4- Insertion dans la table ChoixSpecialisation x 10


-- 5- Insertion dans la table Cours x 10

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

-- 6- Insertion dans la table CoursPrerequis x 10
INSERT INTO CoursPrerequis(id_Cours, id_Prerequis)
VALUES
	(2,1),(3,1),(4,2),(5,2),(6,3),
	(7,1),(8,7),(9,4),(10,5),(6,2);

-- 7- Insertion dans la table CoursProgramme x 10
INSERT INTO CoursProgramme(id_Prog, id_Cours)
VALUES
	(1000,1),(1000,2),(1001,4),(1002,3),(1003,9),
	(1004,3),(1005,6),(1006,7),(1007,8),(1008,10);

-- 8- INsertion dans la table Restreindre x 10
INSERT INTO Restreindre(nb_Restrict, id_Spec, id_Cours)
VALUES
	(2,11,6),(3,19,4),(1,10,10),(2,13,5),(1,16,3),
	(2,17,7),(1,13,8),(3,15,1),(2,18,5),(1,11,9);

-- 9- Insertion dans la table Semestre x 10
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

-- 10- Insertion dans la table CoursOffert x 10
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


-- 11- Insertion dans la table Inscription x 10

-- 12- Insertion dans la table Professeur x 10
INSERT INTO Professeur(id_User)
VALUES
	(109), (113), (118), (119);

-- 13- Insertion dans la table Enseigner x 10

-- 14- Insertion dans la table SessionExamen x 10

-- 15- Insertion dans la table Evaluation x 10

-- 16- Insertion dans la table Note x 10