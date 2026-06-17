using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("CoursProgramme")]
public partial class CoursProgramme
{
    [Key]
    [Column("id_CoursProg")]
    public int IdCoursProg { get; set; }

    [Column("id_Prog")]
    public int IdProg { get; set; }

    [Column("id_Cours")]
    public int IdCours { get; set; }

    [ForeignKey("IdCours")]
    [InverseProperty("CoursProgrammes")]
    public virtual Cour IdCoursNavigation { get; set; } = null!;

    [ForeignKey("IdProg")]
    [InverseProperty("CoursProgrammes")]
    public virtual Programme IdProgNavigation { get; set; } = null!;
}
