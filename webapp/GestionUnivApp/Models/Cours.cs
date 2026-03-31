namespace GestionUnivApp.Models
{
    public class Cours
    {
        public int Id_Cours { get; set; }
        public string Code_Cours { get; set; } =  null!;
        public string Nom_Cours { get; set; } =  null!;
        public int Credit_Cours { get; set; }
        public string Type_Cours { get; set; } =  null!;
        public int? NbHeure_Cours { get; set; }
        public ICollection<CoursProgramme> CoursProgrammes { get; set; } = new List<CoursProgramme>();
        public ICollection<Restreindre> Restrictions { get; set; } = new List<Restreindre>();
        public ICollection<CoursPrerequis> Prerequis { get; set; } = new List<CoursPrerequis>();
        public ICollection<CoursPrerequis> EstPrerequisDe { get; set; } = new List<CoursPrerequis>();
        public ICollection<CoursOffert> CoursOfferts { get; set; } = new List<CoursOffert>();
    }
}
