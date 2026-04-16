using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Administrateur
{
    [Authorize(Roles = "Administrateur")]
    public class AffectationCoursModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AffectationCoursModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProfAvecCours> Professeurs { get; set; } = new();
        public List<CoursOffert> CoursOfferts { get; set; } = new();

        public class ProfAvecCours
        {
            public Utilisateur User { get; set; } = null!;
            public Models.Professeur Prof { get; set; } = null!;
            public List<Enseigner> Enseignements { get; set; } = new();
        }

        public async Task OnGetAsync()
        {
            await ChargerDonnees();
        }

        public async Task<IActionResult> OnPostAssignerAsync(int profId, int coursOfId, int nbHeures)
        {
            // Check not already assigned
            if (await _context.Enseigners.AnyAsync(e => e.IdProf == profId && e.IdCoursOf == coursOfId))
            {
                TempData["Error"] = "Ce professeur enseigne deja ce cours.";
                return RedirectToPage();
            }

            var coursOf = await _context.CoursOfferts
                .Include(co => co.IdSemestNavigation)
                .FirstOrDefaultAsync(co => co.IdCoursOf == coursOfId);

            if (coursOf == null) return NotFound();

            _context.Enseigners.Add(new Enseigner
            {
                IdProf = profId,
                IdCoursOf = coursOfId,
                NbHEnseigner = nbHeures,
                DatDebEnseigner = coursOf.DateDebCoursOf,
                DateFinEnseigner = coursOf.DateFinCoursOf
            });

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cours assigne au professeur.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRetirerAsync(int enseignerId)
        {
            var enseigner = await _context.Enseigners.FindAsync(enseignerId);
            if (enseigner == null) return NotFound();

            _context.Enseigners.Remove(enseigner);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Affectation retiree.";
            return RedirectToPage();
        }

        private async Task ChargerDonnees()
        {
            Professeurs = await _context.Professeurs
                .Include(p => p.IdUserNavigation)
                .Include(p => p.Enseigners)
                    .ThenInclude(e => e.IdCoursOfNavigation)
                        .ThenInclude(co => co.IdCoursNavigation)
                .Include(p => p.Enseigners)
                    .ThenInclude(e => e.IdCoursOfNavigation)
                        .ThenInclude(co => co.IdSemestNavigation)
                .Select(p => new ProfAvecCours
                {
                    User = p.IdUserNavigation,
                    Prof = p,
                    Enseignements = p.Enseigners.ToList()
                })
                .OrderBy(p => p.User.NomUser)
                .ToListAsync();

            CoursOfferts = await _context.CoursOfferts
                .Include(co => co.IdCoursNavigation)
                .Include(co => co.IdSemestNavigation)
                .OrderBy(co => co.IdSemestNavigation.AnneeSemest)
                .ThenBy(co => co.IdCoursNavigation.CodeCours)
                .ToListAsync();
        }
    }
}
