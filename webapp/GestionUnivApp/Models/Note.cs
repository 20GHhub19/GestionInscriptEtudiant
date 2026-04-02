using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Note")]
public partial class Note
{
    [Key]
    [Column("id_Note")]
    public int IdNote { get; set; }

    [Column("valNum_Note", TypeName = "decimal(4, 2)")]
    public decimal ValNumNote { get; set; }

    [Column("ValLettre_Note")]
    [StringLength(2)]
    [Unicode(false)]
    public string ValLettreNote { get; set; } = null!;

    [Column("retroAction")]
    [StringLength(100)]
    [Unicode(false)]
    public string? RetroAction { get; set; }

    [Column("dateAttrib_Note")]
    public DateOnly? DateAttribNote { get; set; }

    [Column("id_Inscript")]
    public int IdInscript { get; set; }

    [Column("id_Eval")]
    public int IdEval { get; set; }

    [ForeignKey("IdEval")]
    [InverseProperty("Notes")]
    public virtual Evaluation IdEvalNavigation { get; set; } = null!;

    [ForeignKey("IdInscript")]
    [InverseProperty("Notes")]
    public virtual Inscription IdInscriptNavigation { get; set; } = null!;
}
