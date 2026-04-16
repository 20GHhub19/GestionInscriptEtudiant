namespace GestionUnivApp.Models.ViewModels
{
public class CoursDetailsVM
{
    public int IdCours { get; set; }
    public string CodeCours { get; set; }
    public string NomCours { get; set; }
    public string Description { get; set; }
    public int Credits { get; set; }

    public ProgrammeResumeVM Programme { get; set; }
    public SpecialisationVM Specialisation { get; set; }
}

}
