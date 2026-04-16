namespace GestionUnivApp.Models.ViewModels
{
    public class SpecialisationDetailsVM
    {
        public int IdSpec { get; set; }
        public string? NomSpec { get; set; }
        public string? Description { get; set; }

        public required ProgrammeResumeVM Programme { get; set; }
        public List<CoursVM> Cours { get; set; } = new();
    }

    public class ProgrammeResumeVM
    {
        public int IdProg { get; set; }
        public string? NomProg { get; set; }
    }

}
