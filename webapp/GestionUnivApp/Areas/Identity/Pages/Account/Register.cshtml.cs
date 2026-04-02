using System.Security.Claims;
using BCrypt.Net;
using GestionUnivApp.Data;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionUnivApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string NomUser { get; set; } = null!;
            public string PrenomUser { get; set; } = null!;
            public DateOnly DateNaisUser { get; set; }
            public string AdresseUser { get; set; } = null!;
            public string NumTelUser { get; set; } = null!;
            public string Role { get; set; } = "Etudiant"; // Valeur par défaut
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // ============================
            // 1. Création de l'utilisateur
            // ============================
            var user = new Utilisateur
            {
                NomUser = Input.NomUser,
                PrenomUser = Input.PrenomUser,
                CourrielUser = Input.Email,
                AdresseUser = Input.AdresseUser,
                NumTelUser = Input.NumTelUser,
                DateNaisUser = Input.DateNaisUser,
                DateInscriptUser = DateOnly.FromDateTime(DateTime.Now),
                PasswordHashUser = BCrypt.Net.BCrypt.HashPassword(Input.Password)
            };

            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync();

            // ============================
            // 2. Ajout du rôle métier
            // ============================
            switch (Input.Role)
            {
                case "Professeur":
                    _context.Professeurs.Add(new Professeur { IdUser = user.IdUser });
                    break;

                case "Administrateur":
                    _context.Administrateurs.Add(new Administrateur
                    {
                        IdUser = user.IdUser
                    });
                    break;

                default:
                    _context.Etudiants.Add(new Etudiant { 
                        IdUser = user.IdUser,
                        StatutEtud = "Inactif"
                        //ProgrammeEtud = 0000
                    });
                    break;
            }

            await _context.SaveChangesAsync();

            // ============================
            // 3. Connexion automatique
            // ============================
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Email, user.CourrielUser),
                new Claim(ClaimTypes.Role, Input.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            return Redirect("~/");
        }
    }
}