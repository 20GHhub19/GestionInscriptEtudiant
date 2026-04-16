# GestionInscriptEtudiant

Systeme de gestion des inscriptions etudiantes, des cours, des notes et des resultats pour une universite.

Application web ASP.NET Core avec base de donnees SQL Server.

## Prerequis

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop/)

## Installation

### 1. Base de donnees (SQL Server via Docker)

Lancer un conteneur SQL Server :

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Gestion123!" \
  -p 1433:1433 --name SQL-Server \
  -d mcr.microsoft.com/mssql/mssql-server-linux:latest
```

> Sur Mac avec Apple Silicon, utiliser `mcr.microsoft.com/azure-sql-edge` a la place.

Attendre quelques secondes que le serveur demarre, puis executer les scripts SQL :

```bash
docker exec -i SQL-Server /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P "Gestion123!" -I \
  < database/LDD_LMD.sql
```

```bash
docker exec -i SQL-Server /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P "Gestion123!" -I -d GestionInscriptEtudiant \
  < database/requetes.sql
```

```bash
docker exec -i SQL-Server /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P "Gestion123!" -I -d GestionInscriptEtudiant \
  < database/T-SQL.sql
```

### 2. Application web

```bash
cd webapp/GestionUnivApp
dotnet restore
dotnet watch
```

L'application sera accessible a `http://localhost:5141` (le port peut varier, verifier la sortie console).

### 3. Compte administrateur

Creer un compte via l'interface, puis le promouvoir en administrateur :

```bash
docker exec -i SQL-Server /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P "Gestion123!" -I -d GestionInscriptEtudiant \
  -Q "DECLARE @id INT = (SELECT id_User FROM Utilisateur WHERE courriel_User = 'VOTRE_EMAIL'); INSERT INTO Administrateur (id_User, role_Admin_Etud) VALUES (@id, 'Administrateur');"
```

Se reconnecter pour activer le role.

## Structure du projet

```
database/
  LDD_LMD.sql          -- Schema et donnees initiales (LDD + LMD)
  requetes.sql         -- 5 requetes simples + 5 requetes complexes
  T-SQL.sql            -- Triggers, fonctions, procedures, curseur, vue

webapp/GestionUnivApp/
  Models/               -- Entites (Utilisateur, Etudiant, Cours, etc.)
  Pages/
    Administrateur/     -- Gestion des utilisateurs, affectation des cours
    Etudiant/           -- Dossier etudiant, inscription aux cours
    Professeur/         -- Saisie des notes
    Programmes/         -- Consultation des programmes et cours
  ApplicationDbContext.cs
  Program.cs
```

## Fonctionnalites

- Inscription et connexion (authentification par cookies)
- Consultation des programmes et de leurs cours
- Inscription aux cours par les etudiants
- Saisie des notes par les professeurs
- Gestion des roles et affectation des cours par l'administrateur
