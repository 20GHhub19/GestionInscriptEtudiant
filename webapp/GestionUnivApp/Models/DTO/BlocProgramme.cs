namespace GestionUnivApp.Models.DTO
{
    public class BlocProgramme
    {
        public int? IdSpec { get; set; }
        public string? NomSpec { get; set; }
        public List<ProgrammesStructureRow> Cours { get; set; } = new();
    }
}
