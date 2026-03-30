namespace GestionUnivApp.Models
{
    public class SessionExamen
    {
        public int Id_SessExam { get; set; }
        public string Type_SessExam { get; set; } = null!;
        public DateTime DateDeb { get; set; }
        public DateTime? DateFin { get; set; }
        public int Id_Semest { get; set; }
        public Semestre Semestre { get; set; } = null!;
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
