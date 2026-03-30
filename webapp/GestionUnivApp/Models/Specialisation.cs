namespace GestionUnivApp.Models
{
    public class Specialisation
    {
        public int Id_Spec { get; set; }
        public string Nom_Spec { get; set; } = null!;
        public string? Description_Spec { get; set; }
        public int NbCredit_Spec { get; set; }
        public int Id_Prog_Spec { get; set; }
        public Programme Programme { get; set; } = null!;
        public ICollection<ChoixSpecialisation> ChoixSpecialisations { get; set; } = new List<ChoixSpecialisation>();
        public ICollection<Restreindre> Restrictions { get; set; } = new List<Restreindre>();
    }
}
