public class ProgrammeDetailsVM
{
    public int IdProg { get; set; }
    public string NomProg { get; set; }
    public string CodeProg { get; set; }
    public DateTime DateCreatProg { get; set; }
    public int DureeProg { get; set; }
    public int NbCreditProg { get; set; }
    public string UniteProg { get; set; }
    public string DescriptProg { get; set; }

    public List<SpecialisationVM> Specialisations { get; set; } = new();
}

public class SpecialisationVM
{
    public int? IdSpec { get; set; }
    public string NomSpec { get; set; }
    public int? NbCreditSpec { get; set; }
    public string DescriptSpec { get; set; }

    public List<CoursVM> Cours { get; set; } = new();
}

public class CoursVM
{
    public int IdCours { get; set; }
    public string NomCours { get; set; }
    public string CodeCours { get; set; }
    public int? NbHeureCours { get; set; }
    public int? CreditCours { get; set; }
    public string TypeCours { get; set; }
}
