using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Programme")]
public partial class Programme
{
    [Key]
    [Column("id_Prog")]
    public int IdProg { get; set; }

    [Column("code_Prog")]
    [StringLength(5)]
    [Unicode(false)]
    public string CodeProg { get; set; } = null!;

    [Column("nom_Prog")]
    [StringLength(30)]
    [Unicode(false)]
    public string NomProg { get; set; } = null!;

    [Column("descript_Prog")]
    [StringLength(400)]
    [Unicode(false)]
    public string? DescriptProg { get; set; }

    [Column("nbCredit_Prog")]
    public int NbCreditProg { get; set; }

    [Column("unite_Prog")]
    [StringLength(30)]
    [Unicode(false)]
    public string? UniteProg { get; set; }

    [Column("duree_Prog")]
    public int? DureeProg { get; set; }

    [Column("dateCreat_Prog")]
    public DateOnly? DateCreatProg { get; set; }

    [InverseProperty("IdProgNavigation")]
    public virtual ICollection<CoursProgramme> CoursProgrammes { get; set; } = new List<CoursProgramme>();

    [InverseProperty("ProgrammeEtudNavigation")]
    public virtual ICollection<Etudiant> Etudiants { get; set; } = new List<Etudiant>();

    [InverseProperty("IdProgSpecNavigation")]
    public virtual ICollection<Specialisation> Specialisations { get; set; } = new List<Specialisation>();
}
