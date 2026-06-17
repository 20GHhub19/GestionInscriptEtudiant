using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace GestionUnivApp.Models;

[Table("Utilisateur")]
[Index("MatUser", Name = "UQ__Utilisat__6FF7022121E542D1", IsUnique = true)]
public partial class Utilisateur
{
    [Key]
    [Column("id_User")]
    public  int IdUser { get; set; } 

    [Column("nom_User")]
    [StringLength(50)]
    [Unicode(false)]
    public string NomUser { get; set; } = null!;

    [Column("prenom_User")]
    [StringLength(50)]
    [Unicode(false)]
    public string PrenomUser { get; set; } = null!;

    [Column("dateInscriptUser")]
    public DateOnly DateInscriptUser { get; set; }

    [Column("mat_User")]
    [Unicode(false)]
    public string? MatUser { get; set; }

    [Column("dateNais_User")]
    public DateOnly DateNaisUser { get; set; }

    [Column("numTel_User")]
    [StringLength(20)]
    [Unicode(false)]
    public string? NumTelUser { get; set; }

    [Column("courriel_User")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CourrielUser { get; set; }

    [Column("passwordHash_User")]
    [StringLength(255)]
    public string? PasswordHashUser { get; set; }

    [Column("adresse_User")]
    [StringLength(200)]
    [Unicode(false)]
    public string? AdresseUser { get; set; }


    [InverseProperty("IdUserNavigation")]
    public virtual Administrateur? Administrateur { get; set; }

    [InverseProperty("IdUserNavigation")]
    public virtual Etudiant? Etudiant { get; set; }

    [InverseProperty("IdUserNavigation")]
    public virtual Professeur? Professeur { get; set; }
}
