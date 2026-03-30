namespace GestionUnivApp.Models
{
    public class Evaluation
    {
        public int Id_Eval { get; set; }
        public string Type_Eval { get; set; } = null!;
        public DateTime Date_Eval { get; set; }
        public string Nom_Eval { get; set; } = null!;
        public decimal? Poids_Eval { get; set; }
        public string? Descript_Eval { get; set; }
        public int Id_CoursOf { get; set; }
        public CoursOffert CoursOffert { get; set; } = null!;
        public int Id_SessExam { get; set; }
        public SessionExamen SessionExamen { get; set; } = null!;
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
