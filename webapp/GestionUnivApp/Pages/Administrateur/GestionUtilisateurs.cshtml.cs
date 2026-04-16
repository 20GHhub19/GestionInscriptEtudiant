using System.Security.Claims;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Administrateur
{
    [Authorize(Roles = "Administrateur")]
    public class GestionUtilisateursModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GestionUtilisateursModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UtilisateurAvecRole> Utilisateurs { get; set; } = new();
        public List<Programme> Programmes { get; set; } = new();

        public class UtilisateurAvecRole
        {
            public Utilisateur User { get; set; } = null!;
            public string? Role { get; set; }
            public string? DetailRole { get; set; }
        }

        public async Task OnGetAsync()
        {
            await ChargerDonnees();
        }

        public async Task<IActionResult> OnPostAssignEtudiantAsync(int userId, int programmeId)
        {
            if (await _context.Etudiants.AnyAsync(e => e.IdUser == userId))
            {
                TempData["Error"] = "Cet utilisateur est deja un etudiant.";
                return RedirectToPage();
            }

            _context.Etudiants.Add(new Models.Etudiant
            {
                IdUser = userId,
                StatutEtud = "Actif",
                ProgrammeEtud = programmeId
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Role etudiant assigne avec succes.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAssignProfesseurAsync(int userId, string grade)
        {
            if (await _context.Professeurs.AnyAsync(p => p.IdUser == userId))
            {
                TempData["Error"] = "Cet utilisateur est deja un professeur.";
                return RedirectToPage();
            }

            _context.Professeurs.Add(new Models.Professeur
            {
                IdUser = userId,
                GradeProf = grade,
                StatutProf = "Permanent"
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Role professeur assigne avec succes.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAssignAdminAsync(int userId, string roleAdmin)
        {
            if (await _context.Administrateurs.AnyAsync(a => a.IdUser == userId))
            {
                TempData["Error"] = "Cet utilisateur est deja un administrateur.";
                return RedirectToPage();
            }

            _context.Administrateurs.Add(new Models.Administrateur
            {
                IdUser = userId,
                RoleAdminEtud = roleAdmin
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Role administrateur assigne avec succes.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(int userId, string role)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (role == "Administrateur" && userId == currentUserId)
            {
                TempData["Error"] = "Vous ne pouvez pas retirer votre propre role administrateur.";
                return RedirectToPage();
            }

            switch (role)
            {
                case "Etudiant":
                    var etud = await _context.Etudiants.FindAsync(userId);
                    if (etud != null) _context.Etudiants.Remove(etud);
                    break;
                case "Professeur":
                    var prof = await _context.Professeurs.FindAsync(userId);
                    if (prof != null) _context.Professeurs.Remove(prof);
                    break;
                case "Administrateur":
                    var admin = await _context.Administrateurs.FindAsync(userId);
                    if (admin != null) _context.Administrateurs.Remove(admin);
                    break;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Role retire avec succes.";
            return RedirectToPage();
        }

        private async Task ChargerDonnees()
        {
            var users = await _context.Utilisateurs
                .Include(u => u.Etudiant)
                    .ThenInclude(e => e!.ProgrammeEtudNavigation)
                .Include(u => u.Professeur)
                .Include(u => u.Administrateur)
                .OrderBy(u => u.NomUser)
                .ToListAsync();

            Utilisateurs = users.Select(u => new UtilisateurAvecRole
            {
                User = u,
                Role = u.Administrateur != null ? "Administrateur"
                     : u.Professeur != null ? "Professeur"
                     : u.Etudiant != null ? "Etudiant"
                     : null,
                DetailRole = u.Etudiant?.ProgrammeEtudNavigation?.NomProg
                          ?? u.Professeur?.GradeProf
                          ?? u.Administrateur?.RoleAdminEtud
            }).ToList();

            Programmes = await _context.Programmes.OrderBy(p => p.NomProg).ToListAsync();
        }
    }
}
