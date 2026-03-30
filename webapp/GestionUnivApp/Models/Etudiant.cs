namespace GestionUnivApp.Models
{
    public class Etudiant : Utilisateur
    {
        public string Statut_Etudiant { get; set; } = null!;
        public int Programme_Etud {  get; set; }
        public Programme? Programme { get; set; }=null!;
        public ICollection<ChoixSpecialisation> ChoixSpecialisations { get; set; } = new List<ChoixSpecialisation>();
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
    }
}
