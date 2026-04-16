using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

public partial class CoursPrerequi
{
    [Key]
    [Column("id_CoursPre")]
    public int IdCoursPre { get; set; }

    [Column("id_Cours")]
    public int IdCours { get; set; }

    [Column("id_Prerequis")]
    public int IdPrerequis { get; set; }

    [ForeignKey("IdCours")]
    [InverseProperty("CoursPrerequiIdCoursNavigations")]
    public virtual Cours IdCoursNavigation { get; set; } = null!;

    [ForeignKey("IdPrerequis")]
    [InverseProperty("CoursPrerequiIdPrerequisNavigations")]
    public virtual Cours IdPrerequisNavigation { get; set; } = null!;
}
