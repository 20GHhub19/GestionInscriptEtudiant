namespace GestionUnivApp.Models
{
    public class Professeur : Utilisateur
    {
        public string Grade_Prof { get; set; } = null!;
        public string Statut_Prof { get; set; } = null!;
        public ICollection<Enseigner> Enseignements { get; set; } = new List<Enseigner>();
    }
}
