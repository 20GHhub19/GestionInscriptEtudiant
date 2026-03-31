
using Microsoft.EntityFrameworkCore.Query;

namespace GestionUnivApp.Models
{
    public class CoursOffert
    {
        public int Id_CoursOf { get; set; }
        public string? Sect_CoursOf { get; set; }
        public int Capacite_CoursOf { get; set; }
        public string Horaire_CoursOf { get; set; } = null!;
        public string? Salle_CoursOf { get; set; }
        public DateTime? DateDeb_CoursOf { get; set; }
        public DateTime? DateFin_CoursOf { get; set; }
        public string MondeEns_CoursOf { get; set; } = null!;
        public int Id_Cours { get; set; }
        public Cours Cours { get; set; } = null!;
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
        public ICollection<Enseigner> Enseignements { get; set; } = new List<Enseigner>();
    }

}
