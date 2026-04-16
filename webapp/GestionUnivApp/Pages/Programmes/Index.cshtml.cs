using GestionUnivApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Programmes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Programme> Programmes { get; set; } = new();

        public async Task OnGetAsync()
        {
            Programmes = await _context.Programmes
                .Include(p => p.Etudiants)
                .Include(p => p.CoursProgrammes)
                .Include(p => p.Specialisations)
                .OrderBy(p => p.CodeProg)
                .ToListAsync();
        }
    }
}
