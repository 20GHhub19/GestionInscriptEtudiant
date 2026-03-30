namespace GestionUnivApp.Models
{
    public class Enseigner
    {
        public int Id_Enseigner { get; set; }
        public int NbH_Enseigner { get; set; }
        public DateTime? DateDeb_Enseigner { get; set; }
        public DateTime? DateFin_Enseigner { get; set; }
        public int Id_Prof { get; set; }
        public Professeur Professeur { get; set; } = null!;
        public int Id_CoursOf { get; set; }
        public CoursOffert CoursOffert { get; set; } = null!;
    }
}
