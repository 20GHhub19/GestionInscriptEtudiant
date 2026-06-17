using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Enseigner")]
public partial class Enseigner
{
    [Key]
    [Column("id_Enseigner")]
    public int IdEnseigner { get; set; }

    [Column("nbH_Enseigner")]
    public int NbHEnseigner { get; set; }

    [Column("datDeb_Enseigner")]
    public DateOnly? DatDebEnseigner { get; set; }

    [Column("dateFin_Enseigner")]
    public DateOnly? DateFinEnseigner { get; set; }

    [Column("id_Prof")]
    public int IdProf { get; set; }

    [Column("id_CoursOf")]
    public int IdCoursOf { get; set; }

    [ForeignKey("IdCoursOf")]
    [InverseProperty("Enseigners")]
    public virtual CoursOffert IdCoursOfNavigation { get; set; } = null!;

    [ForeignKey("IdProf")]
    [InverseProperty("Enseigners")]
    public virtual Professeur IdProfNavigation { get; set; } = null!;
}
