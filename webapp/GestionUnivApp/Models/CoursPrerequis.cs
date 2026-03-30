namespace GestionUnivApp.Models
{
    public class CoursPrerequis
    {
        public int IdèCoursPre { get; set; }
        public int Id_Cours { get; set; }
        public Cours Cours { get; set; } = null!;
        public int Id_Prerequis { get; set; }
        public Cours Prerequis { get; set; } = null!;
    }
}
