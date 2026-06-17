using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Semestre")]
[Index("NomSemest", "AnneeSemest", Name = "UQ_Semestre_nom_annee", IsUnique = true)]
public partial class Semestre
{
    [Key]
    [Column("id_Semest")]
    public int IdSemest { get; set; }

    [Column("nom_Semest")]
    [StringLength(20)]
    [Unicode(false)]
    public string NomSemest { get; set; } = null!;

    [Column("annee_Semest")]
    [StringLength(20)]
    [Unicode(false)]
    public string AnneeSemest { get; set; } = null!;

    [Column("datDeb_Semest")]
    public DateOnly DatDebSemest { get; set; }

    [Column("dateFin_Semest")]
    public DateOnly? DateFinSemest { get; set; }

    [InverseProperty("IdSemestNavigation")]
    public virtual ICollection<CoursOffert> CoursOfferts { get; set; } = new List<CoursOffert>();

    [InverseProperty("IdSemestNavigation")]
    public virtual ICollection<SessionExaman> SessionExamen { get; set; } = new List<SessionExaman>();
}
