namespace GestionUnivApp.Models
{
    public class Inscription
    {
        public int Id_Inscript { get; set; }
        public string? Statut_Inscript { get; set; }
        public DateTime? Date_Inscript { get; set; }
        public DateTime? Date_Desinscript { get; set; }
        public decimal? NoteFi_Inscript { get; set; }
        public string? NoteLet_Inscript { get; set; }
        public string? DecisionFi_Inscript { get; set; }
        public int Id_Etud { get; set; }
        public Etudiant Etudiant { get; set; } = null!;
        public int Id_CoursOf { get; set; }
        public CoursOffert CoursOffert { get; set; } = null!;
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
