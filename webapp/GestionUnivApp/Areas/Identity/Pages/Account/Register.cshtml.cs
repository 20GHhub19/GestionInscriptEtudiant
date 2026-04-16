using System.Security.Claims;
using BCrypt.Net;
using GestionUnivApp.Data;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string NomUser { get; set; } = null!;
            public string PrenomUser { get; set; } = null!;
            public DateOnly DateNaisUser { get; set; }
            public string AdresseUser { get; set; } = null!;
            public string NumTelUser { get; set; } = null!;
            public string Role { get; set; } = "Etudiant"; // Valeur par défaut
            
            public int ProgrammeEtud{  get; set; }
            public List<string> StatutEtud {  get; set; } = new() { "Inactif", "Actif", "Suspendu", "Gradué"};
            public string StatutChoisi {  get; set;} = "Actif";

            public List<string> GradesProfs {  get; set; } = new() { "Professeur Emerite", "Professeur agrégé", "Professeur titulaire", 
                "Maître de conférence", "Chargé de cours", "Auxilliaire de cours" };
            public string GradesParDefaut { get; set; } = "Chargé de cours";

            public List<string> StatutsProfs { get; set; } = new() { "Permanent", "contractuel", "temps plein",
                "temps partiel", "Invité", "Retraité actif" };
            public string statutProfParDefaut { get; set; } = "Permanent";

            public List<string> RoleAdmin { get; set; } = new() { "Administrateur", "Gestionnaire"};
            public string RoleAdminDefaut { get; set; } = "Gestionnaire";
        }

        public List<Programme> Programmes { get; set; } = new();
        public void OnGet() 
        {
            Programmes = _context.Programmes.ToList();
        }

        
        
        public async Task<IActionResult> OnPostAsync()
        {

            if (Input.Role == "Etudiant" && Input.ProgrammeEtud <= 0)
            {
                ModelState.AddModelError("Input.ProgrammeEtud", "Veuillez choisir un programme.");
                Programmes = _context.Programmes.ToList(); // recharger la liste
                return Page();
            }

            if (!ModelState.IsValid)
                return Page();

            // ==========================================================
            // 1. Appel de la procédure stockée pour Créer un utilisateur
            // ===========================================================
            /* var user = new Utilisateur
             {
                 NomUser = Input.NomUser,
                 PrenomUser = Input.PrenomUser,
                 CourrielUser = Input.Email,
                 AdresseUser = Input.AdresseUser,
                 NumTelUser = Input.NumTelUser,
                 DateNaisUser = Input.DateNaisUser,
                 DateInscriptUser = DateOnly.FromDateTime(DateTime.Now),
                 PasswordHashUser = BCrypt.Net.BCrypt.HashPassword(Input.Password)
             };*/
            var idUserParam = new SqlParameter
            {
                ParameterName = "@id_User",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC CreationCompteUtilisateur @nom_User, @prenom_User, @dateNais_User," +
                    "@numTel_User, @passwordHash_User, @courriel_User, @adresse_User, @role," + 
                    " @programme_Etud, @role_Admin_Etud, @statut_Etud, @grade_Prof, @statut_Prof, @id_User OUTPUT",
                    new SqlParameter("@nom_User", Input.NomUser),
                    new SqlParameter("@prenom_User", Input.PrenomUser),
                    new SqlParameter("@dateNais_User", Input.DateNaisUser),
                    new SqlParameter("@numTel_User", Input.NumTelUser),
                    new SqlParameter("@passwordHash_User", BCrypt.Net.BCrypt.HashPassword(Input.Password)),
                    new SqlParameter("@courriel_User", Input.Email),
                    new SqlParameter("@adresse_User", Input.AdresseUser),
                    new SqlParameter("@role", Input.Role),
                    new SqlParameter("@programme_Etud", Input.ProgrammeEtud),
                    new SqlParameter("@role_Admin_Etud", Input.RoleAdminDefaut),
                    new SqlParameter("@statut_Etud", Input.StatutChoisi),
                    new SqlParameter("@grade_Prof", Input.GradesParDefaut),
                    new SqlParameter("@statut_Prof", Input.statutProfParDefaut),
                    idUserParam

                    );
            } catch (SqlException ex)
            {
                // récupération du message d'erreur (RAISERROR) de la procédure stockée
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            int newUserId = (int)idUserParam.Value;
           // _context.Utilisateurs.Add(user);
            //await _context.SaveChangesAsync();

            // ============================
            // 2. Ajout du rôle métier
            // ============================
            /*switch (Input.Role)
            {
                case "Professeur":
                    _context.Professeurs.Add(new Professeur { IdUser = newUserId });
                    break;

                case "Administrateur":
                    _context.Administrateurs.Add(new Administrateur
                    {
                        IdUser = newUserId
                    });
                    break;

                default:
                    _context.Etudiants.Add(new Etudiant { 
                        IdUser = newUserId,
                        StatutEtud = "Inactif"
                        //ProgrammeEtud = 0000
                    });
                    break;
            }
            */

            //await _context.SaveChangesAsync();

            // ============================
            // 3. Connexion automatique
            // ============================
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, newUserId.ToString()),
                new Claim(ClaimTypes.Email, Input.Email),
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