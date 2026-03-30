namespace GestionUnivApp.Models
{
    public class Note
    {
        public int Id_Note { get; set; }
        public decimal ValNum_Note { get; set; }
        public string ValLet_Note { get; set; } = null!;
        public string? RetroAction { get; set; }
        public DateTime? DateAttrib_Note { get; set; }
        public int Id_Inscript { get; set; }
        public Inscription Inscription { get; set; } = null!;
        public int Id_Eval { get; set; }
        public Evaluation Evaluation { get; set; } = null!;
    }
}
