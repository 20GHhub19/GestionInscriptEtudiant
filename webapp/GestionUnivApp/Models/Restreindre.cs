namespace GestionUnivApp.Models
{
    public class Restreindre
    {
        public int If_Rest { get; set; }
        public int Id_Spec { get; set; }
        public Specialisation Specialisation { get; set; } = null!;
        public int Id_Cours { get; set; }
        public Cours Cours { get; set; } = null!;
        public int? Nb_Restrict { get; set; }
    }
}
