using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Etudiant
{
    [Authorize(Roles = "Etudiant")]
    public class EtudiantDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EtudiantDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Utilisateur Utilisateur { get; set; } = null!;
        public Models.Etudiant Etudiant { get; set; } = null!;
        public Programme? Programme { get; set; }
        public List<Inscription> Inscriptions { get; set; } = new();
        public List<Programme> ProgrammesDisponibles { get; set; } = new();
        public List<Specialisation> SpecialisationsDisponibles { get; set; } = new();
        public ChoixSpecialisation? ChoixSpec { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.GetUserId();

            // Utilisateur + Etudiant + Programme en une seule requete
            var etudiant = await _context.Etudiants
                .Include(e => e.IdUserNavigation)
                .Include(e => e.ProgrammeEtudNavigation)
                .FirstOrDefaultAsync(e => e.IdUser == userId);

            if (etudiant == null || etudiant.IdUserNavigation == null)
                return RedirectToPage("/Index");

            Utilisateur = etudiant.IdUserNavigation;
            Etudiant = etudiant;
            Programme = etudiant.ProgrammeEtudNavigation;

            if (Programme == null)
            {
                ProgrammesDisponibles = await _context.Programmes
                    .Include(p => p.Specialisations)
                    .OrderBy(p => p.NomProg)
                    .ToListAsync();
            }
            else
            {
                // Load current specialisation choice
                ChoixSpec = await _context.ChoixSpecialisations
                    .Include(cs => cs.IdSpecNavigation)
                    .Where(cs => cs.IdEtud == userId)
                    .OrderByDescending(cs => cs.DateChoixSpec)
                    .FirstOrDefaultAsync();

                // Load available specialisations for the programme
                SpecialisationsDisponibles = await _context.Specialisations
                    .Where(s => s.IdProgSpec == Etudiant.ProgrammeEtud)
                    .OrderBy(s => s.NomSpec)
                    .ToListAsync();
            }

            Inscriptions = await _context.Inscriptions
                .Include(i => i.IdCoursOfNavigation)
                    .ThenInclude(co => co.IdCoursNavigation)
                .Include(i => i.IdCoursOfNavigation)
                    .ThenInclude(co => co.IdSemestNavigation)
                .Include(i => i.Notes)
                    .ThenInclude(n => n.IdEvalNavigation)
                .Where(i => i.IdEtud == userId)
                .OrderByDescending(i => i.DateInscript)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDesinscriptionAsync(int inscriptionId)
        {
            var userId = User.GetUserId();

            var inscription = await _context.Inscriptions
                .FirstOrDefaultAsync(i => i.IdInscript == inscriptionId && i.IdEtud == userId);

            if (inscription == null) return NotFound();

            inscription.StatutInscript = "Annule";
            inscription.DateDesinscript = DateOnly.FromDateTime(DateTime.Now);

            await _context.SaveChangesAsync();
            TempData["Success"] = "Desinscription effectuee.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChoisirProgrammeAsync(int programmeId, int? specId)
        {
            var userId = User.GetUserId();

            var etudiant = await _context.Etudiants
                .FirstOrDefaultAsync(e => e.IdUser == userId);

            if (etudiant == null) return NotFound();

            etudiant.ProgrammeEtud = programmeId;
            etudiant.StatutEtud = "Actif";

            // Save specialisation choice if provided
            if (specId.HasValue && specId.Value > 0)
            {
                _context.ChoixSpecialisations.Add(new ChoixSpecialisation
                {
                    IdEtud = userId,
                    IdSpec = specId.Value,
                    DateChoixSpec = DateOnly.FromDateTime(DateTime.Now),
                    NbChoixSpec = 1
                });
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Inscription au programme reussie!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChoisirSpecialisationAsync(int specId)
        {
            var userId = User.GetUserId();

            _context.ChoixSpecialisations.Add(new ChoixSpecialisation
            {
                IdEtud = userId,
                IdSpec = specId,
                DateChoixSpec = DateOnly.FromDateTime(DateTime.Now),
                NbChoixSpec = 1
            });

            await _context.SaveChangesAsync();
            TempData["Success"] = "Specialisation choisie!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostQuitterProgrammeAsync()
        {
            var userId = User.GetUserId();

            var etudiant = await _context.Etudiants
                .FirstOrDefaultAsync(e => e.IdUser == userId);

            if (etudiant == null) return NotFound();

            etudiant.ProgrammeEtud = null;
            etudiant.StatutEtud = "Inactif";

            await _context.SaveChangesAsync();
            TempData["Success"] = "Vous avez quitte votre programme.";
            return RedirectToPage();
        }
    }
}
