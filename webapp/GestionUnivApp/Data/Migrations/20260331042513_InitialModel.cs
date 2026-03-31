using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionUnivApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    Id_Cours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code_Cours = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom_Cours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credit_Cours = table.Column<int>(type: "int", nullable: false),
                    Type_Cours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbHeure_Cours = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.Id_Cours);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    Id_Prog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code_Prog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom_Prog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Prog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbCredit_Prog = table.Column<int>(type: "int", nullable: false),
                    Unite_Prog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duree_Prog = table.Column<int>(type: "int", nullable: true),
                    DateCreat_Prog = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.Id_Prog);
                });

            migrationBuilder.CreateTable(
                name: "Semestres",
                columns: table => new
                {
                    Id_Semest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_Semest = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Annee_Semest = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateDeb_Semest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin_Semest = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semestres", x => x.Id_Semest);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom_User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInscriptUser = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mat_User = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateNaissUser = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Courriel_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Num_Tel_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.id_User);
                });

            migrationBuilder.CreateTable(
                name: "CoursOfferts",
                columns: table => new
                {
                    Id_CoursOf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sect_CoursOf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacite_CoursOf = table.Column<int>(type: "int", nullable: false),
                    Horaire_CoursOf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salle_CoursOf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDeb_CoursOf = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateFin_CoursOf = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MondeEns_CoursOf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Cours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursOfferts", x => x.Id_CoursOf);
                    table.ForeignKey(
                        name: "FK_CoursOfferts_Cours_Id_Cours",
                        column: x => x.Id_Cours,
                        principalTable: "Cours",
                        principalColumn: "Id_Cours",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursPrerequis",
                columns: table => new
                {
                    Id_Prerequis = table.Column<int>(type: "int", nullable: false),
                    IdèCoursPre = table.Column<int>(type: "int", nullable: false),
                    Id_Cours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursPrerequis", x => x.Id_Prerequis);
                    table.ForeignKey(
                        name: "FK_CoursPrerequis_Cours_Id_Cours",
                        column: x => x.Id_Cours,
                        principalTable: "Cours",
                        principalColumn: "Id_Cours",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoursPrerequis_Cours_Id_Prerequis",
                        column: x => x.Id_Prerequis,
                        principalTable: "Cours",
                        principalColumn: "Id_Cours",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursProgrammes",
                columns: table => new
                {
                    Id_CoursProg = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Prog = table.Column<int>(type: "int", nullable: false),
                    Id_Cours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursProgrammes", x => x.Id_CoursProg);
                    table.ForeignKey(
                        name: "FK_CoursProgrammes_Cours_Id_Cours",
                        column: x => x.Id_Cours,
                        principalTable: "Cours",
                        principalColumn: "Id_Cours",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursProgrammes_Programmes_Id_Prog",
                        column: x => x.Id_Prog,
                        principalTable: "Programmes",
                        principalColumn: "Id_Prog",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specialisations",
                columns: table => new
                {
                    Id_Spec = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_Spec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Spec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbCredit_Spec = table.Column<int>(type: "int", nullable: false),
                    Id_Prog_Spec = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialisations", x => x.Id_Spec);
                    table.ForeignKey(
                        name: "FK_Specialisations_Programmes_Id_Prog_Spec",
                        column: x => x.Id_Prog_Spec,
                        principalTable: "Programmes",
                        principalColumn: "Id_Prog",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionsExamen",
                columns: table => new
                {
                    Id_SessExam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_SessExam = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateDeb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Semest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionsExamen", x => x.Id_SessExam);
                    table.ForeignKey(
                        name: "FK_SessionsExamen_Semestres_Id_Semest",
                        column: x => x.Id_Semest,
                        principalTable: "Semestres",
                        principalColumn: "Id_Semest",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrateur",
                columns: table => new
                {
                    id_User = table.Column<int>(type: "int", nullable: false),
                    Role_Admin_Etud = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrateur", x => x.id_User);
                    table.ForeignKey(
                        name: "FK_Administrateur_Utilisateur_id_User",
                        column: x => x.id_User,
                        principalTable: "Utilisateur",
                        principalColumn: "id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etudiant",
                columns: table => new
                {
                    id_User = table.Column<int>(type: "int", nullable: false),
                    Statut_Etudiant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Programme_Etud = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiant", x => x.id_User);
                    table.ForeignKey(
                        name: "FK_Etudiant_Programmes_Programme_Etud",
                        column: x => x.Programme_Etud,
                        principalTable: "Programmes",
                        principalColumn: "Id_Prog",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Etudiant_Utilisateur_id_User",
                        column: x => x.id_User,
                        principalTable: "Utilisateur",
                        principalColumn: "id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professeur",
                columns: table => new
                {
                    id_User = table.Column<int>(type: "int", nullable: false),
                    Grade_Prof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut_Prof = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professeur", x => x.id_User);
                    table.ForeignKey(
                        name: "FK_Professeur_Utilisateur_id_User",
                        column: x => x.id_User,
                        principalTable: "Utilisateur",
                        principalColumn: "id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    Id_Rest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Spec = table.Column<int>(type: "int", nullable: false),
                    Id_Cours = table.Column<int>(type: "int", nullable: false),
                    Nb_Restrict = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.Id_Rest);
                    table.ForeignKey(
                        name: "FK_Restrictions_Cours_Id_Cours",
                        column: x => x.Id_Cours,
                        principalTable: "Cours",
                        principalColumn: "Id_Cours",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restrictions_Specialisations_Id_Spec",
                        column: x => x.Id_Spec,
                        principalTable: "Specialisations",
                        principalColumn: "Id_Spec",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id_Eval = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_Eval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Eval = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nom_Eval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poids_Eval = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Descript_Eval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_CoursOf = table.Column<int>(type: "int", nullable: false),
                    Id_SessExam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id_Eval);
                    table.ForeignKey(
                        name: "FK_Evaluations_CoursOfferts_Id_CoursOf",
                        column: x => x.Id_CoursOf,
                        principalTable: "CoursOfferts",
                        principalColumn: "Id_CoursOf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_SessionsExamen_Id_SessExam",
                        column: x => x.Id_SessExam,
                        principalTable: "SessionsExamen",
                        principalColumn: "Id_SessExam",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoixSpecialisations",
                columns: table => new
                {
                    Id_ChoixSpec = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateChoixSpec = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nb_ChoixSpec = table.Column<int>(type: "int", nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false),
                    Id_Spec = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoixSpecialisations", x => x.Id_ChoixSpec);
                    table.ForeignKey(
                        name: "FK_ChoixSpecialisations_Etudiant_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Etudiant",
                        principalColumn: "id_User",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoixSpecialisations_Specialisations_Id_Spec",
                        column: x => x.Id_Spec,
                        principalTable: "Specialisations",
                        principalColumn: "Id_Spec",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscriptions",
                columns: table => new
                {
                    Id_Inscript = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statut_Inscript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Inscript = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Desinscript = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoteFi_Inscript = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoteLet_Inscript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionFi_Inscript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_Etud = table.Column<int>(type: "int", nullable: false),
                    Id_CoursOf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscriptions", x => x.Id_Inscript);
                    table.ForeignKey(
                        name: "FK_Inscriptions_CoursOfferts_Id_CoursOf",
                        column: x => x.Id_CoursOf,
                        principalTable: "CoursOfferts",
                        principalColumn: "Id_CoursOf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Etudiant_Id_Etud",
                        column: x => x.Id_Etud,
                        principalTable: "Etudiant",
                        principalColumn: "id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enseignements",
                columns: table => new
                {
                    Id_Enseigner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NbH_Enseigner = table.Column<int>(type: "int", nullable: false),
                    DateDeb_Enseigner = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateFin_Enseigner = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Prof = table.Column<int>(type: "int", nullable: false),
                    Id_CoursOf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignements", x => x.Id_Enseigner);
                    table.ForeignKey(
                        name: "FK_Enseignements_CoursOfferts_Id_CoursOf",
                        column: x => x.Id_CoursOf,
                        principalTable: "CoursOfferts",
                        principalColumn: "Id_CoursOf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enseignements_Professeur_Id_Prof",
                        column: x => x.Id_Prof,
                        principalTable: "Professeur",
                        principalColumn: "id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id_Note = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValNum_Note = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValLet_Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetroAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAttrib_Note = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Inscript = table.Column<int>(type: "int", nullable: false),
                    Id_Eval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id_Note);
                    table.ForeignKey(
                        name: "FK_Notes_Evaluations_Id_Eval",
                        column: x => x.Id_Eval,
                        principalTable: "Evaluations",
                        principalColumn: "Id_Eval",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Inscriptions_Id_Inscript",
                        column: x => x.Id_Inscript,
                        principalTable: "Inscriptions",
                        principalColumn: "Id_Inscript",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoixSpecialisations_Id_Spec",
                table: "ChoixSpecialisations",
                column: "Id_Spec");

            migrationBuilder.CreateIndex(
                name: "IX_ChoixSpecialisations_Id_User",
                table: "ChoixSpecialisations",
                column: "Id_User");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_Code_Cours",
                table: "Cours",
                column: "Code_Cours",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursOfferts_Id_Cours",
                table: "CoursOfferts",
                column: "Id_Cours");

            migrationBuilder.CreateIndex(
                name: "IX_CoursPrerequis_Id_Cours",
                table: "CoursPrerequis",
                column: "Id_Cours");

            migrationBuilder.CreateIndex(
                name: "IX_CoursProgrammes_Id_Cours",
                table: "CoursProgrammes",
                column: "Id_Cours");

            migrationBuilder.CreateIndex(
                name: "IX_CoursProgrammes_Id_Prog",
                table: "CoursProgrammes",
                column: "Id_Prog");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignements_Id_CoursOf",
                table: "Enseignements",
                column: "Id_CoursOf");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignements_Id_Prof",
                table: "Enseignements",
                column: "Id_Prof");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiant_Programme_Etud",
                table: "Etudiant",
                column: "Programme_Etud");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Id_CoursOf",
                table: "Evaluations",
                column: "Id_CoursOf");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Id_SessExam",
                table: "Evaluations",
                column: "Id_SessExam");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_Id_CoursOf",
                table: "Inscriptions",
                column: "Id_CoursOf");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_Id_Etud_Id_CoursOf",
                table: "Inscriptions",
                columns: new[] { "Id_Etud", "Id_CoursOf" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Id_Eval",
                table: "Notes",
                column: "Id_Eval");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Id_Inscript",
                table: "Notes",
                column: "Id_Inscript");

            migrationBuilder.CreateIndex(
                name: "IX_Restrictions_Id_Cours",
                table: "Restrictions",
                column: "Id_Cours");

            migrationBuilder.CreateIndex(
                name: "IX_Restrictions_Id_Spec",
                table: "Restrictions",
                column: "Id_Spec");

            migrationBuilder.CreateIndex(
                name: "IX_Semestres_Nom_Semest_Annee_Semest",
                table: "Semestres",
                columns: new[] { "Nom_Semest", "Annee_Semest" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionsExamen_Id_Semest",
                table: "SessionsExamen",
                column: "Id_Semest");

            migrationBuilder.CreateIndex(
                name: "IX_SessionsExamen_Type_SessExam_DateDeb",
                table: "SessionsExamen",
                columns: new[] { "Type_SessExam", "DateDeb" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialisations_Id_Prog_Spec",
                table: "Specialisations",
                column: "Id_Prog_Spec");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Utilisateur",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_Mat_User",
                table: "Utilisateur",
                column: "Mat_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Utilisateur",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Utilisateur_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Utilisateur",
                principalColumn: "id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Utilisateur_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Utilisateur",
                principalColumn: "id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Utilisateur_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Utilisateur",
                principalColumn: "id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Utilisateur_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Utilisateur",
                principalColumn: "id_User",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Utilisateur_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Utilisateur_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Utilisateur_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Utilisateur_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Administrateur");

            migrationBuilder.DropTable(
                name: "ChoixSpecialisations");

            migrationBuilder.DropTable(
                name: "CoursPrerequis");

            migrationBuilder.DropTable(
                name: "CoursProgrammes");

            migrationBuilder.DropTable(
                name: "Enseignements");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "Professeur");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Inscriptions");

            migrationBuilder.DropTable(
                name: "Specialisations");

            migrationBuilder.DropTable(
                name: "SessionsExamen");

            migrationBuilder.DropTable(
                name: "CoursOfferts");

            migrationBuilder.DropTable(
                name: "Etudiant");

            migrationBuilder.DropTable(
                name: "Semestres");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
