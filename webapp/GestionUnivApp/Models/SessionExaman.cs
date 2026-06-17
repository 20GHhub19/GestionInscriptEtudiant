using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Index("TypeSessExam", "DateDeb", Name = "UQ_SessionExamen_type_dateDeb", IsUnique = true)]
public partial class SessionExaman
{
    [Key]
    [Column("id_SessExam")]
    public int IdSessExam { get; set; }

    [Column("type_SessExam")]
    [StringLength(15)]
    [Unicode(false)]
    public string TypeSessExam { get; set; } = null!;

    [Column("dateDeb")]
    public DateOnly DateDeb { get; set; }

    [Column("dateFin")]
    public DateOnly? DateFin { get; set; }

    [Column("id_Semest")]
    public int IdSemest { get; set; }

    [InverseProperty("IdSessExamNavigation")]
    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    [ForeignKey("IdSemest")]
    [InverseProperty("SessionExamen")]
    public virtual Semestre IdSemestNavigation { get; set; } = null!;
}
