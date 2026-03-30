namespace GestionUnivApp.Models
{
    public class ChoixSpecialisation
    {
        public int Id_ChoixSpec { get; set; }
        public DateTime DateChoixSpec { get; set; }
        public int Nb_ChoixSpec { get; set; }
        public  int Id_User { get; set; }
        public Etudiant Etudiant { get; set; } = null!;
        public int Id_Spec { get; set; }
        public Specialisation Specialisation { get; set; } = null!;
    }
}
