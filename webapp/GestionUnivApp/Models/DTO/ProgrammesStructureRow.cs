namespace GestionUnivApp.Models.DTO
{
    public class ProgrammesStructureRow
    {
        //Programme
        public int IdProg { get; set; }
        public string NomProg { get; set; } = string.Empty; 
        public string CodeProg { get; set; } = string.Empty;
        
        //Spécialisation (peut être NULL)
        public int? IdSpec { get; set; }
        public int? IdProgSpec { get; set; }
        public string? NomSpec { get; set; }
        public int? NbCrdSpec { get; set; }

        //Cours (peut être NULL)
        public int? IdCours { get; set; }
        public string? CodeCours { get; set; } = null;
        public string? NomCours { get; set; }
        public int? CredCours { get; set; }
        public string? TypeCours { get; set; }

        //Indicateur de la ligne (pour éviter les doublons dans l'affichage)
        public string TypeLien { get; set; } = string.Empty; // "Programme", "Spécialisation" ou "Cours"
    }
}
