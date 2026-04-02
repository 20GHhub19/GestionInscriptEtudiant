using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("CoursOffert")]
public partial class CoursOffert
{
    [Key]
    [Column("id_CoursOf")]
    public int IdCoursOf { get; set; }

    [Column("sect_CoursOf")]
    [StringLength(3)]
    [Unicode(false)]
    public string? SectCoursOf { get; set; }

    [Column("capacite_CoursOf")]
    public int CapaciteCoursOf { get; set; }

    [Column("horaire_CoursOf")]
    [StringLength(20)]
    [Unicode(false)]
    public string HoraireCoursOf { get; set; } = null!;

    [Column("salle_CoursOf")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SalleCoursOf { get; set; }

    [Column("dateDeb_CoursOf")]
    public DateOnly? DateDebCoursOf { get; set; }

    [Column("dateFin_CoursOf")]
    public DateOnly? DateFinCoursOf { get; set; }

    [Column("mondeEns_CoursOf")]
    [StringLength(30)]
    [Unicode(false)]
    public string? MondeEnsCoursOf { get; set; }

    [Column("id_Cours")]
    public int IdCours { get; set; }

    [InverseProperty("IdCoursOfNavigation")]
    public virtual ICollection<Enseigner> Enseigners { get; set; } = new List<Enseigner>();

    [InverseProperty("IdCoursOfNavigation")]
    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    [ForeignKey("IdCours")]
    [InverseProperty("CoursOfferts")]
    public virtual Cour IdCoursNavigation { get; set; } = null!;

    [InverseProperty("IdCoursOfNavigation")]
    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}
