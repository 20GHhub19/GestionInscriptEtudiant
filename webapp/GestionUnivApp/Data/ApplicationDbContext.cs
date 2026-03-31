using GestionUnivApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<Utilisateur, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // --- DbSets ---
        public DbSet<Programme> Programmes => Set<Programme>();
        public DbSet<Specialisation> Specialisations => Set<Specialisation>();

        public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();
        public DbSet<Etudiant> Etudiants => Set<Etudiant>();
        public DbSet<Professeur> Professeurs => Set<Professeur>();
        public DbSet<Administrateur> Administrateurs => Set<Administrateur>();

        public DbSet<Cours> Cours => Set<Cours>();
        public DbSet<CoursProgramme> CoursProgrammes => Set<CoursProgramme>();
        public DbSet<CoursPrerequis> CoursPrerequis => Set<CoursPrerequis>();
        public DbSet<CoursOffert> CoursOfferts => Set<CoursOffert>();

        public DbSet<Semestre> Semestres => Set<Semestre>();
        public DbSet<SessionExamen> SessionsExamen => Set<SessionExamen>();
        public DbSet<Evaluation> Evaluations => Set<Evaluation>();

        public DbSet<ChoixSpecialisation> ChoixSpecialisations => Set<ChoixSpecialisation>();
        public DbSet<Inscription> Inscriptions => Set<Inscription>();
        public DbSet<Note> Notes => Set<Note>();

        public DbSet<Restreindre> Restrictions => Set<Restreindre>();
        public DbSet<Enseigner> Enseignements => Set<Enseigner>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Héritage TPT ---
            modelBuilder.Entity<Utilisateur>().ToTable("Utilisateur");
            modelBuilder.Entity<Etudiant>().ToTable("Etudiant");
            modelBuilder.Entity<Professeur>().ToTable("Professeur");
            modelBuilder.Entity<Administrateur>().ToTable("Administrateur");

            // --- Renommage de la PK Identity ---
            modelBuilder.Entity<Utilisateur>()
                .Property(u => u.Id)
                .HasColumnName("id_User");

            // --- Index uniques ---
            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Mat_User)
                .IsUnique();

            modelBuilder.Entity<Cours>()
                .HasKey(c => c.Id_Cours);
            modelBuilder.Entity<Cours>()
                .HasIndex(c => c.Code_Cours)
                .IsUnique();

            modelBuilder.Entity<Semestre>()
                .HasKey(s => s.Id_Semest);
            modelBuilder.Entity<Semestre>()
                .HasIndex(s => new { s.Nom_Semest, s.Annee_Semest })
                .IsUnique();

            modelBuilder.Entity<SessionExamen>()
                .HasIndex(se => new { se.Type_SessExam, se.DateDeb })
                .IsUnique();

            modelBuilder.Entity<Inscription>()
                .HasIndex(i => new { i.Id_Etud, i.Id_CoursOf })
                .IsUnique();

            // --- Programme ---
            modelBuilder.Entity<Programme>()
                .HasKey(p => p.Id_Prog);

            modelBuilder.Entity<Programme>()
                .Property(p => p.DateCreat_Prog)
                .HasDefaultValueSql("GETDATE()");

            // --- Relations principales ---
            modelBuilder.Entity<Specialisation>()
               .HasKey(s => s.Id_Spec);

            modelBuilder.Entity<Specialisation>()
                .HasOne(s => s.Programme)
                .WithMany(p => p.Specialisations)
                .HasForeignKey(s => s.Id_Prog_Spec);

            modelBuilder.Entity<Etudiant>()
                .HasOne(e => e.Programme)
                .WithMany(p => p.Etudiants)
                .HasForeignKey(e => e.Programme_Etud);

            modelBuilder.Entity<CoursProgramme>()
                .HasKey(cp => cp.Id_CoursProg);

            modelBuilder.Entity<CoursProgramme>()
                .HasOne(cp => cp.Programme)
                .WithMany(p => p.CoursProgrammes)
                .HasForeignKey(cp => cp.Id_Prog);

            modelBuilder.Entity<CoursProgramme>()
                .HasOne(cp => cp.Cours)
                .WithMany(c => c.CoursProgrammes)
                .HasForeignKey(cp => cp.Id_Cours);

            // --- Prérequis ---
            modelBuilder.Entity<CoursPrerequis>()
                .HasOne(cp => cp.Cours)
                .WithMany(c => c.Prerequis)
                .HasForeignKey(cp => cp.Id_Cours)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoursPrerequis>()
                .HasKey(cp => cp.Id_Prerequis);

            modelBuilder.Entity<CoursPrerequis>()
                .HasOne(cp => cp.Prerequis)
                .WithMany(c => c.EstPrerequisDe)
                .HasForeignKey(cp => cp.Id_Prerequis)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Cours offert ---
            modelBuilder.Entity<CoursOffert>()
                .HasKey(co => co.Id_CoursOf);

            modelBuilder.Entity<CoursOffert>()
                .HasOne(co => co.Cours)
                .WithMany(c => c.CoursOfferts)
                .HasForeignKey(co => co.Id_Cours);

            // --- Session examen ---

            modelBuilder.Entity<SessionExamen>()
               .HasKey(se => se.Id_SessExam);


            modelBuilder.Entity<SessionExamen>()
                .HasOne(se => se.Semestre)
                .WithMany(s => s.SessionExamens)
                .HasForeignKey(se => se.Id_Semest);

            // --- Evaluation ---
            modelBuilder.Entity<Evaluation>()
                .HasKey(e => e.Id_Eval);

            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.CoursOffert)
                .WithMany(co => co.Evaluations)
                .HasForeignKey(e => e.Id_CoursOf);

            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.SessionExamen)
                .WithMany(se => se.Evaluations)
                .HasForeignKey(e => e.Id_SessExam);

            // --- Choix de spécialisation ---
            modelBuilder.Entity<ChoixSpecialisation>()
                .HasKey(cs => cs.Id_ChoixSpec);

            modelBuilder.Entity<ChoixSpecialisation>()
                .HasOne(cs => cs.Etudiant)
                .WithMany(e => e.ChoixSpecialisations)
                .HasForeignKey(cs => cs.Id_User);

            modelBuilder.Entity<ChoixSpecialisation>()
                .HasOne(cs => cs.Specialisation)
                .WithMany(s => s.ChoixSpecialisations)
                .HasForeignKey(cs => cs.Id_Spec);

            // --- Inscription ---
            modelBuilder.Entity<Inscription>()
               .HasKey(i => i.Id_Inscript);


            modelBuilder.Entity<Inscription>()
                .HasOne(i => i.Etudiant)
                .WithMany(e => e.Inscriptions)
                .HasForeignKey(i => i.Id_Etud);

            modelBuilder.Entity<Inscription>()
                .HasOne(i => i.CoursOffert)
                .WithMany(co => co.Inscriptions)
                .HasForeignKey(i => i.Id_CoursOf);

            // --- Notes ---
            modelBuilder.Entity<Note>()
               .HasKey(n => n.Id_Note);


            modelBuilder.Entity<Note>()
                .HasOne(n => n.Inscription)
                .WithMany(i => i.Notes)
                .HasForeignKey(n => n.Id_Inscript);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Evaluation)
                .WithMany(e => e.Notes)
                .HasForeignKey(n => n.Id_Eval);

            // --- Restrictions ---
            modelBuilder.Entity<Restreindre>()
                .HasKey(r => r.Id_Rest);

            modelBuilder.Entity<Restreindre>()
                .HasOne(r => r.Specialisation)
                .WithMany(s => s.Restrictions)
                .HasForeignKey(r => r.Id_Spec);

            modelBuilder.Entity<Restreindre>()
                .HasOne(r => r.Cours)
                .WithMany(c => c.Restrictions)
                .HasForeignKey(r => r.Id_Cours);

            // --- Enseignements ---
            modelBuilder.Entity<Enseigner>()
                .HasKey(e => e.Id_Enseigner);


            modelBuilder.Entity<Enseigner>()
                .HasOne(e => e.Professeur)
                .WithMany(p => p.Enseignements)
                .HasForeignKey(e => e.Id_Prof);

            modelBuilder.Entity<Enseigner>()
                .HasOne(e => e.CoursOffert)
                .WithMany(co => co.Enseignements)
                .HasForeignKey(e => e.Id_CoursOf);
        }
    }
}