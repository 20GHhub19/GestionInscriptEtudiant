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
	WHERE NAME = 'FK_Specialisation_Programme' OR NAME = 'FK_Etudiant_Programme' OR NAME = 'FK_CoursProgramme_Programme' OR NAME = 'FK_CoursProgramme_Cours' 
			OR NAME = 'FK_CoursPrerequis_Cours' OR NAME = 'FK_CoursPrerequis_Prerequis' OR NAME = 'FK_CoursOffert_Cours' OR NAME = 'FK_CoursOffert_Semestre' OR NAME = 'FK_SessionExamen_Semestre' 
			OR NAME = 'FK_Evaluation_CoursOffert' OR NAME = 'FK_Evaluation_SessionExamen' OR NAME = 'FK_ChoixSpecialisation_Etudiant' OR NAME = 'FK_ChoixSpecialisation_Specialisation'
			OR NAME = 'FK_Inscription_Etudiant' OR NAME = 'FK_Inscription_CoursOffert' OR NAME = 'FK_Note_Inscription' OR NAME = 'FK_Note_Evaluation'
			OR NAME = 'FK_Restreindre_Specialisation' OR NAME = 'FK_Restreindre_Cours' OR NAME = 'FK_Enseigner_Professeur' OR NAME = 'FK_Enseigner_CoursOffert'
)
BEGIN
	ALTER TABLE Specialisation
	DROP CONSTRAINT FK_Specialisation_Programme;

	ALTER TABLE Etudiant
	DROP CONSTRAINT FK_Etudiant_Programme

	ALTER TABLE CoursProgramme
	DROP CONSTRAINT FK_CoursProgramme_Programme, FK_CoursProgramme_Cours

	ALTER TABLE CoursPrerequis
	DROP CONSTRAINT FK_CoursPrerequis_Cours, FK_CoursPrerequis_Prerequis;

	ALTER TABLE CoursOffert
	DROP CONSTRAINT FK_CoursOffert_Cours, FK_CoursOffert_Semestre;

	ALTER TABLE SessionExamen
	DROP CONSTRAINT FK_SessionExamen_Semestre;

	ALTER TABLE Evaluation
	DROP CONSTRAINT FK_Evaluation_CoursOffert, FK_Evaluation_SessionExamen;

	ALTER TABLE ChoixSpecialisation
	DROP CONSTRAINT FK_ChoixSpecialisation_Etudiant, FK_ChoixSpecialisation_Specialisation;

	ALTER TABLE Inscription
	DROP CONSTRAINT FK_Inscription_Etudiant, FK_Inscription_CoursOffert;

	ALTER TABLE Note
	DROP CONSTRAINT FK_Note_Inscription, FK_Note_Evaluation;

	ALTER TABLE Restreindre
	DROP CONSTRAINT FK_Restreindre_Specialisation, FK_Restreindre_Cours;

	ALTER TABLE Enseigner
	DROP CONSTRAINT FK_Enseigner_Professeur, FK_Enseigner_CoursOffert;
END

IF EXISTS (
	SELECT 1
	FROM sys.key_constraints
	WHERE NAME = 'PK_Programme'
)
BEGIN
	ALTER TABLE Programme
	DROP CONSTRAINT PK_Programme
END


DROP TABLE IF EXISTS Programme;
DROP TABLE IF EXISTS Specialisation;
DROP TABLE IF EXISTS Etudiant;
DROP TABLE IF EXISTS Cours;
DROP TABLE IF EXISTS CoursProgramme;
DROP TABLE IF EXISTS CoursPrerequis;
DROP TABLE IF EXISTS CoursOffert;
DROP TABLE IF EXISTS Semestre;
DROP TABLE IF EXISTS SessionExamen;
DROP TABLE IF EXISTS Evaluation;
DROP TABLE IF EXISTS Professeur;
DROP TABLE IF EXISTS ChoixSpecialisation;
DROP TABLE IF EXISTS Inscription;
DROP TABLE IF EXISTS Note;
DROP TABLE IF EXISTS Restreindre;
DROP TABLE IF EXISTS Enseigner;

CREATE TABLE Programme (
	id_Prog INT IDENTITY(1000, 1) NOT NULL,
	code_Prog VARCHAR(5) NOT NULL,
	nom_Prog VARCHAR(30) NOT NULL,
	descript_Prog VARCHAR(400),
	nbCredit_Prog INT NOT NULL,
	unite_Prog VARCHAR(30),
	duree_Prog INT,
	dateCreat_Prog DATE CONSTRAINT DF_Prog_dateCreat_Prog DEFAULT CAST(GETDATE() AS  DATE),
	CONSTRAINT PK_Programme PRIMARY KEY(id_Prog),
);

INSERT INTO Programme(code_Prog, nom_Prog, descript_Prog, nbCredit_Prog, unite_Prog, duree_Prog)
VALUES
('INF01','Informatique','Programme informatique',90,'Departement TI',3),
('NET01','Reseaux','Programme reseaux',60,'Departement TI',2);

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

-- 3-) Table Etudiant
CREATE TABLE Etudiant (
	id_Etud INT IDENTITY(100, 1) NOT NULL,
	dateInscriptProg DATE NOT NULL DEFAULT GETDATE(),
	mat_Etud AS (CAST(YEAR(dateInscriptProg) AS VARCHAR(4)) +
	RIGHT('00' + CAST(MONTH(dateInscriptProg) AS VARCHAR(2)), 2) +
	RIGHT('0000' + CAST(id_Etud AS VARCHAR(4)), 4)) PERSISTED UNIQUE,
	nom_Etud VARCHAR(30) NOT NULL,
	prenom_Etud VARCHAR(30),
	dateNais_Etud DATE NOT NULL,
	numTel_Etud VARCHAR(15),
	CHECK(numTel_Etud LIKE '\([1-9][0-9][0-9]\)-[1-9][0-9][0-9]-[1-9][0-9][0-9][0-9]' ESCAPE '\' AND LEN(numTel_Etud) = 14 ),
	courriel_Etud VARCHAR(30),
	CHECK(courriel_Etud LIKE '[A-Za-z0-9._]___%@[A-Za-z0-9]___%.[A-Za-z0-9]_%'),
	adresse_Etud VARCHAR(80) NOT NULL,
	statut_Etud VARCHAR(30) NOT NULL,
	programme_Etud INT NOT NULL,
	CONSTRAINT PK_Etudiant PRIMARY KEY (id_Etud),
	CONSTRAINT FK_Etudiant_Programme FOREIGN KEY(programme_Etud) REFERENCES Programme(id_Prog),
)

INSERT INTO Etudiant (nom_Etud, prenom_Etud, dateNais_Etud, numTel_Etud, courriel_Etud, adresse_Etud, statut_Etud, programme_Etud)
VALUES (
    'Dupont', 
    'Jean', 
    '2004-05-12', 
    '(514)-123-4567', 
    'jean.dupont@yahoo.com', 
    '123 Rue Principale, Montréal, QC', 
    'Actif', 
    1001  -- id du programme existant dans la table Programme
	),
	(
    'Gaius', 
    'Heumen', 
    '1999-08-08', 
    '(581)-123-4567', 
    'gaius.heument@gmail.com', 
    '123 Rue Principale, Montréal, QC', 
    'inscrit', 
    1000  -- id du programme existant dans la table Programme
);

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

-- 7-) Création de la table Semestre

CREATE TABLE Semestre (
	id_Semest INT IDENTITY (1, 1),
	nom_Semest VARCHAR(20) NOT NULL,
	annee_Semest VARCHAR(20) NOT NULL,
	datDeb_Semest DATE NOT NULL,
	dateFin_Semest DATE,
	CONSTRAINT UQ_Semestre_nom_annee UNIQUE(nom_Semest, annee_Semest),
	CONSTRAINT PK_Semestre PRIMARY KEY(id_Semest)
)

-- 8- Création de la table CoursOffert

CREATE TABLE CoursOffert (
	id_CoursOf INT IDENTITY(1, 1),
	sect_CoursOf VARCHAR(3) DEFAULT 'A',
	capacite_CoursOf INT NOT NULL,
	horaire_CoursOf VARCHAR(10) NOT NULL,
	salle_CoursOf VARCHAR(15),
	dateDeb_CourOf DATE,
	dateFin_CourOf DATE,
	mondeEns_CoursOf VARCHAR(30) DEFAULT 'Présentiel',
	id_Cours INT NOT NULL,
	id_Semest INT NOT NULL,
	CONSTRAINT CK_CoursOf_mondeEns_CoursOf CHECK (mondeEns_CoursOf IN ('Présentiel', 'Asynchrone', 'En ligne', 'Bimodal')),
	CONSTRAINT PK_CoursOffert PRIMARY KEY(id_CoursOf),
	CONSTRAINT FK_CoursOffert_Cours FOREIGN KEY(id_Cours) REFERENCES Cours(id_Cours),
	CONSTRAINT FK_CoursOffert_Semestre FOREIGN KEY(id_Semest) REFERENCES Semestre(id_Semest)
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
	CONSTRAINT CK_Evaluation_type_Eval CHECK(type_Eval IN ('Travaux Pratiques', 'Intra', 'Quiz', 'Examen final')),
	CONSTRAINT PK_Evaluation PRIMARY KEY(id_Eval),
	CONSTRAINT FK_Evaluation_CoursOffert FOREIGN KEY(id_CoursOf) REFERENCES CoursOffert(id_CoursOf),
	CONSTRAINT FK_Evaluation_SessionExamen FOREIGN KEY(id_SessExam) REFERENCES SessionExamen(id_SessExam)
)

-- 11-) Création de la table Professeur

CREATE TABLE Professeur (
	id_Prof INT IDENTITY(4, 1000),
	dateEmb_Prof DATE NOT NULL DEFAULT GETDATE(),
	code_Prof AS (CAST(YEAR(dateEmb_Prof) AS VARCHAR(4)) +
	RIGHT('00' + CAST(MONTH(dateEmb_Prof) AS VARCHAR(2)), 2) +
	RIGHT('0000' + CAST(id_Prof AS VARCHAR(4)), 4)) PERSISTED UNIQUE,
	nom_Prof VARCHAR(15) NOT NULL,
	prenom_Prof VARCHAR(20) NOT NULL,
	dateNais_Prof Date NOT NULL,
	numTel_Prof VARCHAR(15),
	CHECK(numTel_Prof LIKE '\([1-9][0-9][0-9]\)-[1-9][0-9][0-9]-[1-9][0-9][0-9][0-9]' ESCAPE '\' AND LEN(numTel_Prof) = 14 ),
	courriel_Prof VARCHAR(30),
	CHECK(courriel_Prof LIKE '[A-Za-z0-9._]___%@[A-Za-z0-9]___%.[A-Za-z0-9]_%'), 
	adresse_Prof  VARCHAR(80),
	grade_Prof VARCHAR(50) DEFAULT 'Chargé de cours',
	statut_Prof VARCHAR(30) DEFAULT 'Permanent',
	CONSTRAINT PK_Professeur PRIMARY KEY(id_Prof),
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
	CONSTRAINT FK_ChoixSpecialisation_Etudiant FOREIGN KEY(id_Etud) REFERENCES Etudiant(id_Etud),
	CONSTRAINT FK_ChoixSpecialisation_Specialisation FOREIGN KEY(id_Spec) REFERENCES Specialisation(id_Spec)
)

-- 13-) Création de la table Inscription

CREATE TABLE Inscription (
	id_Inscript INT IDENTITY(10, 1),
	statut_Inscipt VARCHAR(20),
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
	CONSTRAINT CK_Inscription_decisionFi CHECK(decisionFi_Inscript IN ('Réussi', 'Échoué', 'Abandon', 'Incomplet')),
	CONSTRAINT FK_Inscription_Etudiant FOREIGN KEY(id_Etud) REFERENCES Etudiant(id_Etud),
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
	CONSTRAINT FK_Enseigner_Professeur FOREIGN KEY(id_Prof) REFERENCES Professeur(id_Prof),
	CONSTRAINT FK_Enseigner_CoursOffert FOREIGN KEY(id_CoursOf) REFERENCES CoursOffert(id_CoursOf)
)

/*
1- Spécification des colonnes calculées
https://learn.microsoft.com/fr-fr/sql/relational-databases/tables/specify-computed-columns-in-a-table?view=sql-server-ver17
https://www.sqlservertutorial.net/sql-server-basics/sql-server-computed-columns/

2- Comment utiliser les pattern matching avec LIKE ET ESCAPE 
https://learn.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql?view=sql-server-ver17
https://www.sqlservertutorial.net/sql-server-basics/sql-server-like/
*/