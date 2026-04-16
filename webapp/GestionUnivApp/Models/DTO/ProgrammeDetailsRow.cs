namespace GestionUnivApp.Models.DTO
{
    public class ProgrammeDetailsRow
    {
        public int IdProg { get; set; }
        public string NomProg { get; set; }
        public string CodeProg { get; set; }
        public DateTime DateCreatProg { get; set; }
        public int DureeProg { get; set; }
        public int NbCreditProg { get; set; }
        public string UniteProg { get; set; }
        public string DescriptProg { get; set; }

        public int? IdSpec { get; set; }
        public string NomSpec { get; set; }
        public int? NbCreditSpec { get; set; }
        public string DescriptSpec { get; set; }

        public int? IdCours { get; set; }
        public string NomCours { get; set; }
        public string CodeCours { get; set; }
        public int? NbHeureCours { get; set; }
        public int? CreditCours { get; set; }
        public string TypeCours { get; set; }
    }

}
