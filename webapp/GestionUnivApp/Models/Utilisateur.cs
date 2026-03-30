using Microsoft.AspNetCore.Identity;

namespace GestionUnivApp.Models
{
    public abstract class Utilisateur : IdentityUser<int>
    {
        public string Nom_User { get; set; } = null!;
        public string Prenom_User { get; set; } = null!;
        public DateTime DateInscriptUser { get; set; }
        public string Mat_User { get; set; } = null!;
        public DateTime DateNaissUser { get; set; }
        public string? Courriel_User { get; set; }
        public string? Num_Tel_User { get; set; }
        public string? Adresse_User { get; set; }
    }
}
