using GestionUnivApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int NbEtudiants { get; set; }
        public int NbProfesseurs { get; set; }
        public int NbCours { get; set; }
        public int NbProgrammes { get; set; }

        public async Task OnGetAsync()
        {
            NbEtudiants = await _context.Etudiants.CountAsync();
            NbProfesseurs = await _context.Professeurs.CountAsync();
            NbCours = await _context.Cours.CountAsync();
            NbProgrammes = await _context.Programmes.CountAsync();
        }
    }
}
