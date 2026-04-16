using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GestionUnivApp.Models;
using GestionUnivApp.Models.DTO;

namespace GestionUnivApp;

public partial class ApplicationDbContext : DbContext //: IdentityDbContext<Utilisateur, IdentityRole<int>, int>  //DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrateur> Administrateurs { get; set; }

    public virtual DbSet<ChoixSpecialisation> ChoixSpecialisations { get; set; }

    public virtual DbSet<Cours> Cours { get; set; }

    public virtual DbSet<CoursOffert> CoursOfferts { get; set; }

    public virtual DbSet<CoursPrerequi> CoursPrerequis { get; set; }

    public virtual DbSet<CoursProgramme> CoursProgrammes { get; set; }

    public virtual DbSet<Enseigner> Enseigners { get; set; }

    public virtual DbSet<Etudiant> Etudiants { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Professeur> Professeurs { get; set; }

    public virtual DbSet<Programme> Programmes { get; set; }

    public virtual DbSet<Restreindre> Restreindres { get; set; }

    public virtual DbSet<Semestre> Semestres { get; set; }

    public virtual DbSet<SessionExaman> SessionExamen { get; set; }

    public virtual DbSet<Specialisation> Specialisations { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<ProgrammesStructureRow> ProgrammesStructureRows { get; set; }

    public DbSet<ProgrammeDetailsRow> ProgrammeDetailsRows { get; set; }


    /*
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
         => optionsBuilder.UseSqlServer("Server=GAIUSH;Database=GestionInscriptEtudiant;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configuration de Identity pour Utilisateur
        base.OnModelCreating(modelBuilder); // Nécessaire pour la configuration de Identity

        modelBuilder.Entity<Administrateur>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK_Administarteur");

            entity.Property(e => e.IdUser).ValueGeneratedNever();

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Administrateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrateur_Utilisateur");
        });

        modelBuilder.Entity<ChoixSpecialisation>(entity =>
        {
            entity.HasOne(d => d.IdEtudNavigation).WithMany(p => p.ChoixSpecialisations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChoixSpecialisation_Etudiant");

            entity.HasOne(d => d.IdSpecNavigation).WithMany(p => p.ChoixSpecialisations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChoixSpecialisation_Specialisation");
        });

        modelBuilder.Entity<Cours>(entity =>
        {
            entity.Property(e => e.TypeCours).HasDefaultValue("Théorique");
        });

        modelBuilder.Entity<CoursOffert>(entity =>
        {
            entity.Property(e => e.MondeEnsCoursOf).HasDefaultValue("Présentiel");
            entity.Property(e => e.SectCoursOf).HasDefaultValue("A");

            entity.HasOne(d => d.IdCoursNavigation).WithMany(p => p.CoursOfferts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursOffert_Cours");
        });

        modelBuilder.Entity<CoursPrerequi>(entity =>
        {
            entity.HasOne(d => d.IdCoursNavigation).WithMany(p => p.CoursPrerequiIdCoursNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursPrerequis_Cours");

            entity.HasOne(d => d.IdPrerequisNavigation).WithMany(p => p.CoursPrerequiIdPrerequisNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursPrerequis_Prerequis");
        });

        modelBuilder.Entity<CoursProgramme>(entity =>
        {
            entity.HasOne(d => d.IdCoursNavigation).WithMany(p => p.CoursProgrammes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursProgramme_Cours");

            entity.HasOne(d => d.IdProgNavigation).WithMany(p => p.CoursProgrammes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursProgramme_Programme");
        });

        modelBuilder.Entity<Enseigner>(entity =>
        {
            entity.HasOne(d => d.IdCoursOfNavigation).WithMany(p => p.Enseigners)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enseigner_CoursOffert");

            entity.HasOne(d => d.IdProfNavigation).WithMany(p => p.Enseigners)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enseigner_Professeur");
        });

        modelBuilder.Entity<Etudiant>(entity =>
        {
            entity.Property(e => e.IdUser).ValueGeneratedNever();

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Etudiant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Etudiant_Utilisateur");

            entity.HasOne(d => d.ProgrammeEtudNavigation).WithMany(p => p.Etudiants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Etudiant_Programme");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.Property(e => e.TypeEval).HasDefaultValue("Travaux Pratiques");

            entity.HasOne(d => d.IdCoursOfNavigation).WithMany(p => p.Evaluations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evaluation_CoursOffert");

            entity.HasOne(d => d.IdSessExamNavigation).WithMany(p => p.Evaluations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evaluation_SessionExamen");
        });

        modelBuilder.Entity<Inscription>(entity =>
        {
            entity.Property(e => e.DateInscript).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TentativeInscript).HasDefaultValue(1);

            entity.HasOne(d => d.IdCoursOfNavigation).WithMany(p => p.Inscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscription_CoursOffert");

            entity.HasOne(d => d.IdEtudNavigation).WithMany(p => p.Inscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscription_Etudiant");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasOne(d => d.IdEvalNavigation).WithMany(p => p.Notes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Note_Evaluation");

            entity.HasOne(d => d.IdInscriptNavigation).WithMany(p => p.Notes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Note_Inscription");
        });

        modelBuilder.Entity<Professeur>(entity =>
        {
            entity.Property(e => e.IdUser).ValueGeneratedNever();
            entity.Property(e => e.GradeProf).HasDefaultValue("Chargé de cours");
            entity.Property(e => e.StatutProf).HasDefaultValue("Permanent");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Professeur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Professeur_Utilisateur");
        });

        modelBuilder.Entity<Programme>(entity =>
        {
            entity.Property(e => e.DateCreatProg).HasDefaultValueSql("(CONVERT([date],getdate()))", "DF_Prog_dateCreat_Prog");
        });

        modelBuilder.Entity<Restreindre>(entity =>
        {
            entity.HasOne(d => d.IdCoursNavigation).WithMany(p => p.Restreindres)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Restreindre_Cours");

            entity.HasOne(d => d.IdSpecNavigation).WithMany(p => p.Restreindres)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Restreindre_Specialisation");
        });

        modelBuilder.Entity<SessionExaman>(entity =>
        {
            entity.HasOne(d => d.IdSemestNavigation).WithMany(p => p.SessionExamen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SessionExamen_Semestre");
        });

        modelBuilder.Entity<Specialisation>(entity =>
        {
            entity.HasOne(d => d.IdProgSpecNavigation).WithMany(p => p.Specialisations).HasConstraintName("FK_Specialisation_Programme");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.Property(e => e.IdUser).HasColumnName("id_User"); // Mapper la propriété Id de IdentityUser<int>
                                                                 // à id_User dans la base de données
            entity.Property(e => e.DateInscriptUser).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MatUser).HasComputedColumnSql("((CONVERT([varchar](4),datepart(year,[dateInscriptUser]))+right('00'+CONVERT([varchar](2),datepart(month,[dateInscriptUser])),(2)))+right('0000'+CONVERT([varchar](4),[id_User]),(4)))", true);
        });

        modelBuilder.Entity<ProgrammesStructureRow>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null); // Indique que ce n'est pas une table ou une vue dans la base de données
        });

        modelBuilder.Entity<ProgrammeDetailsRow>()
        .HasNoKey();


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
