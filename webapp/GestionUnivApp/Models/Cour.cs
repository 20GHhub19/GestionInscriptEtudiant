using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Index("CodeCours", Name = "UQ__Cours__434D099AB748230D", IsUnique = true)]
public partial class Cour
{
    [Key]
    [Column("id_Cours")]
    public int IdCours { get; set; }

    [Column("code_Cours")]
    [StringLength(5)]
    [Unicode(false)]
    public string CodeCours { get; set; } = null!;

    [Column("nom_Cours")]
    [StringLength(30)]
    [Unicode(false)]
    public string NomCours { get; set; } = null!;

    [Column("credit_Cours")]
    public int CreditCours { get; set; }

    [Column("type_Cours")]
    [StringLength(30)]
    [Unicode(false)]
    public string TypeCours { get; set; } = null!;

    [Column("nbHeure_Cours")]
    public int? NbHeureCours { get; set; }

    [InverseProperty("IdCoursNavigation")]
    public virtual ICollection<CoursOffert> CoursOfferts { get; set; } = new List<CoursOffert>();

    [InverseProperty("IdCoursNavigation")]
    public virtual ICollection<CoursPrerequi> CoursPrerequiIdCoursNavigations { get; set; } = new List<CoursPrerequi>();

    [InverseProperty("IdPrerequisNavigation")]
    public virtual ICollection<CoursPrerequi> CoursPrerequiIdPrerequisNavigations { get; set; } = new List<CoursPrerequi>();

    [InverseProperty("IdCoursNavigation")]
    public virtual ICollection<CoursProgramme> CoursProgrammes { get; set; } = new List<CoursProgramme>();

    [InverseProperty("IdCoursNavigation")]
    public virtual ICollection<Restreindre> Restreindres { get; set; } = new List<Restreindre>();
}
