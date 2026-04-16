using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Restreindre")]
public partial class Restreindre
{
    [Key]
    [Column("id_Rest")]
    public int IdRest { get; set; }

    [Column("nb_Restrict")]
    public int? NbRestrict { get; set; }

    [Column("id_Spec")]
    public int IdSpec { get; set; }

    [Column("id_Cours")]
    public int IdCours { get; set; }

    [ForeignKey("IdCours")]
    [InverseProperty("Restreindres")]
    public virtual Cours IdCoursNavigation { get; set; } = null!;

    [ForeignKey("IdSpec")]
    [InverseProperty("Restreindres")]
    public virtual Specialisation IdSpecNavigation { get; set; } = null!;
}
