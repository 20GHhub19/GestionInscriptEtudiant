namespace GestionUnivApp.Models
{
    public class Semestre
    {
        public int Id_Semest { get; set; }
        public string Nom_Semest { get; set; } = null!;
        public string Annee_Semest { get; set; } = null!;
        public DateTime DateDeb_Semest { get; set; }
        public DateTime DateFin_Semest { get; set; }
        public ICollection<SessionExamen> SessionExamens { get; set; } = new List<SessionExamen>();

    }
}
