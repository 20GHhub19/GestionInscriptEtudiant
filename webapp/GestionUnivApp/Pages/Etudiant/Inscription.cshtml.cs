using System.Security.Claims;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Etudiant
{
    [Authorize(Roles = "Etudiant")]
    public class InscriptionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InscriptionModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Etudiant Etudiant { get; set; } = null!;
        public Programme Programme { get; set; } = null!;
        public List<CoursOffert> CoursDisponibles { get; set; } = new();
        public List<int> DejaInscritCoursOfIds { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var etudiant = await _context.Etudiants
                .Include(e => e.ProgrammeEtudNavigation)
                .FirstOrDefaultAsync(e => e.IdUser == userId);

            if (etudiant == null || etudiant.ProgrammeEtud == null)
                return RedirectToPage("EtudiantDashboard");

            Etudiant = etudiant;
            Programme = etudiant.ProgrammeEtudNavigation;

            var coursIdsDansProgramme = await _context.CoursProgrammes
                .Where(cp => cp.IdProg == Etudiant.ProgrammeEtud)
                .Select(cp => cp.IdCours)
                .ToListAsync();

            CoursDisponibles = await _context.CoursOfferts
                .Include(co => co.IdCoursNavigation)
                .Include(co => co.IdSemestNavigation)
                .Include(co => co.Inscriptions)
                .Include(co => co.Enseigners)
                    .ThenInclude(e => e.IdProfNavigation)
                        .ThenInclude(p => p.IdUserNavigation)
                .Where(co => coursIdsDansProgramme.Contains(co.IdCours))
                .OrderBy(co => co.IdSemestNavigation.AnneeSemest)
                .ThenBy(co => co.IdSemestNavigation.NomSemest)
                .ThenBy(co => co.IdCoursNavigation.CodeCours)
                .ToListAsync();

            DejaInscritCoursOfIds = await _context.Inscriptions
                .Where(i => i.IdEtud == userId && i.StatutInscript == "Inscrit")
                .Select(i => i.IdCoursOf)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostInscrireAsync(int coursOfId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Check for existing inscription (unique constraint)
            var existante = await _context.Inscriptions
                .FirstOrDefaultAsync(i => i.IdEtud == userId && i.IdCoursOf == coursOfId);

            if (existante != null)
            {
                if (existante.StatutInscript == "Inscrit")
                {
                    TempData["Error"] = "Vous etes deja inscrit a ce cours.";
                    return RedirectToPage();
                }
                // Re-enroll cancelled inscription
                existante.StatutInscript = "Inscrit";
                existante.DateInscript = DateOnly.FromDateTime(DateTime.Now);
                existante.DateDesinscript = null;
                existante.TentativeInscript = (existante.TentativeInscript ?? 0) + 1;
                existante.NoteFiInscript = null;
                existante.NoteLetInscript = null;
                existante.DecisionFiInscript = null;
            }
            else
            {
                // Check capacity
                var coursOf = await _context.CoursOfferts
                    .Include(co => co.Inscriptions)
                    .FirstOrDefaultAsync(co => co.IdCoursOf == coursOfId);

                if (coursOf == null) return NotFound();

                var nbInscrits = coursOf.Inscriptions.Count(i => i.StatutInscript == "Inscrit");
                if (nbInscrits >= coursOf.CapaciteCoursOf)
                {
                    TempData["Error"] = "Ce cours est complet.";
                    return RedirectToPage();
                }

                _context.Inscriptions.Add(new Models.Inscription
                {
                    IdEtud = userId,
                    IdCoursOf = coursOfId,
                    StatutInscript = "Inscrit",
                    DateInscript = DateOnly.FromDateTime(DateTime.Now),
                    TentativeInscript = 1
                });
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Inscription reussie!";
            return RedirectToPage();
        }
    }
}
