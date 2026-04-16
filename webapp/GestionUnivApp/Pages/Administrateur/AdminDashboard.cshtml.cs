using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Administrateur
{
    [Authorize(Roles = "Administrateur")]
    public class AdminDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int NbEtudiants { get; set; }
        public int NbProfesseurs { get; set; }
        public int NbCours { get; set; }
        public int NbProgrammes { get; set; }
        public int NbInscriptions { get; set; }

        public List<EtudiantInfo> Etudiants { get; set; } = new();
        public List<ProfesseurInfo> Professeurs { get; set; } = new();
        public List<Programme> Programmes { get; set; } = new();
        public List<Inscription> DernieresInscriptions { get; set; } = new();

        public class EtudiantInfo
        {
            public Utilisateur User { get; set; } = null!;
            public Models.Etudiant Etudiant { get; set; } = null!;
            public string? NomProgramme { get; set; }
        }

        public class ProfesseurInfo
        {
            public Utilisateur User { get; set; } = null!;
            public Models.Professeur Prof { get; set; } = null!;
            public int NbCours { get; set; }
        }

        public async Task OnGetAsync()
        {
            NbEtudiants = await _context.Etudiants.CountAsync();
            NbProfesseurs = await _context.Professeurs.CountAsync();
            NbCours = await _context.Cours.CountAsync();
            NbProgrammes = await _context.Programmes.CountAsync();
            NbInscriptions = await _context.Inscriptions.CountAsync();

            Etudiants = await _context.Etudiants
                .Include(e => e.IdUserNavigation)
                .Include(e => e.ProgrammeEtudNavigation)
                .Select(e => new EtudiantInfo
                {
                    User = e.IdUserNavigation,
                    Etudiant = e,
                    NomProgramme = e.ProgrammeEtudNavigation != null ? e.ProgrammeEtudNavigation.NomProg : null
                })
                .ToListAsync();

            Professeurs = await _context.Professeurs
                .Include(p => p.IdUserNavigation)
                .Include(p => p.Enseigners)
                .Select(p => new ProfesseurInfo
                {
                    User = p.IdUserNavigation,
                    Prof = p,
                    NbCours = p.Enseigners.Count
                })
                .ToListAsync();

            Programmes = await _context.Programmes
                .Include(p => p.Etudiants)
                .ToListAsync();

            DernieresInscriptions = await _context.Inscriptions
                .Include(i => i.IdEtudNavigation)
                    .ThenInclude(e => e.IdUserNavigation)
                .Include(i => i.IdCoursOfNavigation)
                    .ThenInclude(co => co.IdCoursNavigation)
                .OrderByDescending(i => i.DateInscript)
                .Take(10)
                .ToListAsync();
        }
    }
}
