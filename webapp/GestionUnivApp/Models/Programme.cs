namespace GestionUnivApp.Models
{
    public class Programme
    {
        public int Id_Prog { get; set; }
        public string code_Prog { get; set; } =  null!;
        public string Nom_Prog { get; set; } =  null!;
        public string? Description_Prog { get; set; }
        public int NbCredit_Prog { get; set; }
        public string? Unite_Prog { get; set; }
        public int? Duree_Prog { get; set; }
        public DateTime? DateCreat_Prog { get; set; }
        public ICollection<Specialisation> Specialisations { get; set; } = new List<Specialisation>();
        public ICollection<Etudiant> Etudiants { get; set; } = new List<Etudiant>();
        public ICollection<CoursProgramme> CoursProgrammes { get; set; } = new List<CoursProgramme>();


    }
}
