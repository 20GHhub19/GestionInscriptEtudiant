using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Evaluation")]
public partial class Evaluation
{
    [Key]
    [Column("id_Eval")]
    public int IdEval { get; set; }

    [Column("type_Eval")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TypeEval { get; set; }

    [Column("date_Eval")]
    public DateOnly DateEval { get; set; }

    [Column("nom_Eval")]
    [StringLength(50)]
    [Unicode(false)]
    public string NomEval { get; set; } = null!;

    [Column("poids_Eval", TypeName = "decimal(4, 2)")]
    public decimal? PoidsEval { get; set; }

    [Column("descript_Eval")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DescriptEval { get; set; }

    [Column("id_CoursOf")]
    public int IdCoursOf { get; set; }

    [Column("id_SessExam")]
    public int IdSessExam { get; set; }

    [ForeignKey("IdCoursOf")]
    [InverseProperty("Evaluations")]
    public virtual CoursOffert IdCoursOfNavigation { get; set; } = null!;

    [ForeignKey("IdSessExam")]
    [InverseProperty("Evaluations")]
    public virtual SessionExaman IdSessExamNavigation { get; set; } = null!;

    [InverseProperty("IdEvalNavigation")]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
