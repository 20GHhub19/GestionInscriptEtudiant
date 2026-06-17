using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Inscription")]
[Index("IdEtud", "IdCoursOf", Name = "UQ_Inscription_id_Etud_id_CoursOf", IsUnique = true)]
public partial class Inscription
{
    [Key]
    [Column("id_Inscript")]
    public int IdInscript { get; set; }

    [Column("statut_Inscript")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StatutInscript { get; set; }

    [Column("date_Inscript")]
    public DateOnly? DateInscript { get; set; }

    [Column("date_Desinscript")]
    public DateOnly? DateDesinscript { get; set; }

    [Column("noteFi_Inscript", TypeName = "decimal(4, 2)")]
    public decimal? NoteFiInscript { get; set; }

    [Column("noteLet_Inscript")]
    [StringLength(2)]
    [Unicode(false)]
    public string? NoteLetInscript { get; set; }

    [Column("decisionFi_Inscript")]
    [StringLength(20)]
    [Unicode(false)]
    public string? DecisionFiInscript { get; set; }

    [Column("tentative_Inscript")]
    public int? TentativeInscript { get; set; }

    [Column("estValidePrerequis_Inscript")]
    public bool? EstValidePrerequisInscript { get; set; }

    [Column("id_Etud")]
    public int IdEtud { get; set; }

    [Column("id_CoursOf")]
    public int IdCoursOf { get; set; }

    [ForeignKey("IdCoursOf")]
    [InverseProperty("Inscriptions")]
    public virtual CoursOffert IdCoursOfNavigation { get; set; } = null!;

    [ForeignKey("IdEtud")]
    [InverseProperty("Inscriptions")]
    public virtual Etudiant IdEtudNavigation { get; set; } = null!;

    [InverseProperty("IdInscriptNavigation")]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
