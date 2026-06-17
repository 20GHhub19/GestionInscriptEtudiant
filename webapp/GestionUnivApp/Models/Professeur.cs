using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Professeur")]
public partial class Professeur
{
    [Key]
    [Column("id_User")]
    public int IdUser { get; set; }

    [Column("grade_Prof")]
    [StringLength(50)]
    [Unicode(false)]
    public string? GradeProf { get; set; }

    [Column("statut_Prof")]
    [StringLength(30)]
    [Unicode(false)]
    public string? StatutProf { get; set; }

    [InverseProperty("IdProfNavigation")]
    public virtual ICollection<Enseigner> Enseigners { get; set; } = new List<Enseigner>();

    [ForeignKey("IdUser")]
    [InverseProperty("Professeur")]
    public virtual Utilisateur IdUserNavigation { get; set; } = null!;
}
