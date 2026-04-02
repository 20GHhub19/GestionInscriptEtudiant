using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("ChoixSpecialisation")]
public partial class ChoixSpecialisation
{
    [Key]
    [Column("id_ChoixSpec")]
    public int IdChoixSpec { get; set; }

    [Column("date_ChoixSpec")]
    public DateOnly DateChoixSpec { get; set; }

    [Column("nb_ChoixSpec")]
    public int? NbChoixSpec { get; set; }

    [Column("id_Etud")]
    public int IdEtud { get; set; }

    [Column("id_Spec")]
    public int IdSpec { get; set; }

    [ForeignKey("IdEtud")]
    [InverseProperty("ChoixSpecialisations")]
    public virtual Etudiant IdEtudNavigation { get; set; } = null!;

    [ForeignKey("IdSpec")]
    [InverseProperty("ChoixSpecialisations")]
    public virtual Specialisation IdSpecNavigation { get; set; } = null!;
}
