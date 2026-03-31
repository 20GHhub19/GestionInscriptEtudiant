namespace GestionUnivApp.Models
{
    public class CoursProgramme
    {
        public int Id_CoursProg { get; set; }
        public int Id_Prog { get; set; }
        public Programme Programme { get; set; } = null!;
        public int Id_Cours { get; set; }
        public Cours Cours { get; set; } = null!;
    }
}
