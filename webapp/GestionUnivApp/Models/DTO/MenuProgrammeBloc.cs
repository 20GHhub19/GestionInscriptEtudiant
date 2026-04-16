namespace GestionUnivApp.Models.DTO
{
    public class MenuProgrammeBloc
    {
       // public Programme Programme { get; set; } = new();
        public List<BlocProgramme> Blocs { get; set; } = new();
        public int IdProg { get; internal set; }
        public string? NomProg { get; internal set; } = null;
        public string? CodeProg { get; internal set; } = null;
    }
}
