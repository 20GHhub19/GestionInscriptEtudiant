using System.Data;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
            var userId = User.GetUserId();

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
            var userId = User.GetUserId();

            // Re-enrollment path: if a cancelled inscription exists for the same course,
            // reactivate it in-app (the stored procedure would be blocked by the UNIQUE constraint).
            var existante = await _context.Inscriptions
                .FirstOrDefaultAsync(i => i.IdEtud == userId && i.IdCoursOf == coursOfId);

            if (existante != null)
            {
                if (existante.StatutInscript == "Inscrit")
                {
                    TempData["Error"] = "Vous etes deja inscrit a ce cours.";
                    return RedirectToPage();
                }

                existante.StatutInscript = "Inscrit";
                existante.DateInscript = DateOnly.FromDateTime(DateTime.Now);
                existante.DateDesinscript = null;
                existante.TentativeInscript = (existante.TentativeInscript ?? 0) + 1;
                existante.NoteFiInscript = null;
                existante.NoteLetInscript = null;
                existante.DecisionFiInscript = null;

                await _context.SaveChangesAsync();
                TempData["Success"] = "Inscription reussie!";
                return RedirectToPage();
            }

            // New enrollment: delegate to the stored procedure sp_InscrireEtudiantCours,
            // which validates capacity and prerequisites inside a transaction.
            var idEtudParam = new SqlParameter("@id_Etud", userId);
            var coursOfParam = new SqlParameter("@id_CoursOf", coursOfId);
            var idInscriptOut = new SqlParameter
            {
                ParameterName = "@id_Inscript",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InscrireEtudiantCours @id_Etud, @id_CoursOf, @id_Inscript OUTPUT",
                    idEtudParam, coursOfParam, idInscriptOut);

                TempData["Success"] = "Inscription reussie!";
            }
            catch (SqlException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToPage();
        }
    }
}
