using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Specialisation")]
public partial class Specialisation
{
    [Key]
    [Column("id_Spec")]
    public int IdSpec { get; set; }

    [Column("nom_Spec")]
    [StringLength(30)]
    [Unicode(false)]
    public string NomSpec { get; set; } = null!;

    [Column("descript_Spec")]
    [StringLength(400)]
    [Unicode(false)]
    public string? DescriptSpec { get; set; }

    [Column("nbCredit_Spec")]
    public int NbCreditSpec { get; set; }

    [Column("id_Prog_Spec")]
    public int? IdProgSpec { get; set; }

    [InverseProperty("IdSpecNavigation")]
    public virtual ICollection<ChoixSpecialisation> ChoixSpecialisations { get; set; } = new List<ChoixSpecialisation>();

    [ForeignKey("IdProgSpec")]
    [InverseProperty("Specialisations")]
    public virtual Programme? IdProgSpecNavigation { get; set; }

    [InverseProperty("IdSpecNavigation")]
    public virtual ICollection<Restreindre> Restreindres { get; set; } = new List<Restreindre>();
}
