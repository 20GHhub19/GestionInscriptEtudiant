using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
            [Required, EmailAddress]
            public string Email { get; set; } = null!;

            [Required, MinLength(6)]
            public string Password { get; set; } = null!;

            [Required, StringLength(50)]
            public string NomUser { get; set; } = null!;

            [Required, StringLength(50)]
            public string PrenomUser { get; set; } = null!;

            [Required]
            public DateOnly DateNaisUser { get; set; }

            [Required, StringLength(200)]
            public string AdresseUser { get; set; } = null!;

            [StringLength(20)]
            public string NumTelUser { get; set; } = null!;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Check email uniqueness
            if (await _context.Utilisateurs.AnyAsync(u => u.CourrielUser == Input.Email))
            {
                ModelState.AddModelError(string.Empty, "Cette adresse courriel est deja utilisee.");
                return Page();
            }

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

            // Auto-login (no role claim — admin assigns roles)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, $"{Input.PrenomUser} {Input.NomUser}"),
                new Claim(ClaimTypes.Email, Input.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return Redirect("~/");
        }
    }
}
