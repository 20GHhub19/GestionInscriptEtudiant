using System.Security.Claims;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.CourrielUser == Input.Email);

            if (utilisateur == null || string.IsNullOrEmpty(utilisateur.PasswordHashUser)
                                    || !BCrypt.Net.BCrypt.Verify(Input.Password, utilisateur.PasswordHashUser))
            {
                ModelState.AddModelError(string.Empty, "Courriel ou mot de passe invalide.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, utilisateur.IdUser.ToString()),
                new(ClaimTypes.Name, $"{utilisateur.PrenomUser} {utilisateur.NomUser}"),
                new(ClaimTypes.Email, utilisateur.CourrielUser!)
            };

            if (await _context.Administrateurs.AnyAsync(a => a.IdUser == utilisateur.IdUser))
                claims.Add(new Claim(ClaimTypes.Role, "Administrateur"));
            if (await _context.Professeurs.AnyAsync(p => p.IdUser == utilisateur.IdUser))
                claims.Add(new Claim(ClaimTypes.Role, "Professeur"));
            if (await _context.Etudiants.AnyAsync(e => e.IdUser == utilisateur.IdUser))
                claims.Add(new Claim(ClaimTypes.Role, "Etudiant"));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return Redirect("~/");
        }
    }
}
