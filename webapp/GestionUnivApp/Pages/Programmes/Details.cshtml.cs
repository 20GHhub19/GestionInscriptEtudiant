using GestionUnivApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Programmes
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Programme Programme { get; set; } = null!;
        public List<CoursProgramme> CoursDuProgramme { get; set; } = new();
        public List<Specialisation> Specialisations { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var prog = await _context.Programmes
                .FirstOrDefaultAsync(p => p.IdProg == id);

            if (prog == null) return NotFound();
            Programme = prog;

            CoursDuProgramme = await _context.CoursProgrammes
                .Include(cp => cp.IdCoursNavigation)
                    .ThenInclude(c => c.CoursPrerequiIdCoursNavigations)
                        .ThenInclude(pr => pr.IdPrerequisNavigation)
                .Include(cp => cp.IdCoursNavigation)
                    .ThenInclude(c => c.CoursOfferts)
                        .ThenInclude(co => co.IdSemestNavigation)
                .Where(cp => cp.IdProg == id)
                .OrderBy(cp => cp.IdCoursNavigation.CodeCours)
                .ToListAsync();

            Specialisations = await _context.Specialisations
                .Where(s => s.IdProgSpec == id)
                .ToListAsync();

            return Page();
        }
    }
}
