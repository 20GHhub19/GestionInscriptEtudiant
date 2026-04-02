using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Etudiant")]
public partial class Etudiant
{
    [Key]
    [Column("id_User")]
    public int IdUser { get; set; }

    [Column("statut_Etud")]
    [StringLength(30)]
    [Unicode(false)]
    public required string StatutEtud { get; set; }

    [Column("programme_Etud")]
    public int? ProgrammeEtud { get; set; }

    [InverseProperty("IdEtudNavigation")]
    public virtual ICollection<ChoixSpecialisation> ChoixSpecialisations { get; set; } = new List<ChoixSpecialisation>();

    [ForeignKey("IdUser")]
    [InverseProperty("Etudiant")]
    public virtual Utilisateur IdUserNavigation { get; set; } = null!;

    [InverseProperty("IdEtudNavigation")]
    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    [ForeignKey("ProgrammeEtud")]
    [InverseProperty("Etudiants")]
    public virtual Programme ProgrammeEtudNavigation { get; set; } = null!;
}
