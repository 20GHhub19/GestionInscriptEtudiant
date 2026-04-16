using System.Security.Claims;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Professeur
{
    [Authorize(Roles = "Professeur")]
    public class ProfDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Utilisateur Utilisateur { get; set; } = null!;
        public Models.Professeur Prof { get; set; } = null!;
        public List<Enseigner> Enseignements { get; set; } = new();
        public int TotalEtudiants { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.IdUser == userId);
            var prof = await _context.Professeurs.FirstOrDefaultAsync(p => p.IdUser == userId);

            if (utilisateur == null || prof == null) return RedirectToPage("/Index");

            Utilisateur = utilisateur;
            Prof = prof;

            Enseignements = await _context.Enseigners
                .Include(e => e.IdCoursOfNavigation)
                    .ThenInclude(co => co.IdCoursNavigation)
                .Include(e => e.IdCoursOfNavigation)
                    .ThenInclude(co => co.IdSemestNavigation)
                .Include(e => e.IdCoursOfNavigation)
                    .ThenInclude(co => co.Inscriptions)
                        .ThenInclude(i => i.IdEtudNavigation)
                            .ThenInclude(et => et.IdUserNavigation)
                .Where(e => e.IdProf == userId)
                .ToListAsync();

            TotalEtudiants = Enseignements
                .SelectMany(e => e.IdCoursOfNavigation.Inscriptions)
                .Select(i => i.IdEtud)
                .Distinct()
                .Count();

            return Page();
        }

        public async Task<IActionResult> OnPostSaveNoteAsync(int inscriptionId, decimal? noteFi, string? noteLet, string? decision)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var inscription = await _context.Inscriptions
                .Include(i => i.IdCoursOfNavigation)
                    .ThenInclude(co => co.Enseigners)
                .FirstOrDefaultAsync(i => i.IdInscript == inscriptionId);

            if (inscription == null) return NotFound();

            // Security: verify this prof teaches the course
            bool teachesThisCourse = inscription.IdCoursOfNavigation
                .Enseigners.Any(e => e.IdProf == userId);
            if (!teachesThisCourse) return Forbid();

            inscription.NoteFiInscript = noteFi;
            inscription.NoteLetInscript = noteLet;
            inscription.DecisionFiInscript = decision;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Note sauvegardee.";
            return RedirectToPage();
        }
    }
}
