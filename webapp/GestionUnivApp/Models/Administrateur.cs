using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Models;

[Table("Administrateur")]
public partial class Administrateur
{
    [Key]
    [Column("id_User")]
    public int IdUser { get; set; }

    [Column("role_Admin_Etud")]
    [StringLength(30)]
    [Unicode(false)]
    public string RoleAdminEtud { get; set; } = null!;

    [ForeignKey("IdUser")]
    [InverseProperty("Administrateur")]
    public virtual Utilisateur IdUserNavigation { get; set; } = null!;
}
