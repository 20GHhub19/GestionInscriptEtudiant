using BCrypt.Net;
using GestionUnivApp.Data;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            //var user = _context.Utilisateurs
              //  .FirstOrDefault(u => u.CourrielUser == Input.Email);
            var user = _context.Utilisateurs
                .Include(u => u.Administrateur)
                .Include(u => u.Professeur)
                .FirstOrDefault(u => u.CourrielUser == Input.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(Input.Password, user.PasswordHashUser))
            {
                ModelState.AddModelError(string.Empty, "Courriel ou mot de passe invalide.");
                return Page();
            }

            string role =
                user.Administrateur != null ? "Administrateur" :
                user.Professeur != null ? "Professeur" :
                "Etudiant";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Email, user.CourrielUser),
                new Claim(ClaimTypes.Role, role)
            };

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            var identity = new ClaimsIdentity(claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return role switch
            {
                "Administrateur" => RedirectToPage("/Administrateur/AdminDashboard"),
                "Professeur" => RedirectToPage("/Professeur/ProfDashboard"),
                _ => RedirectToPage("/Etudiant/EtudiantDashboard")
            };
        }
    }
}