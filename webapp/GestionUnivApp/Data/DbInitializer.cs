using Microsoft.AspNetCore.Identity;
namespace GestionUnivApp.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = 
                services.GetRequiredService<RoleManager<IdentityRole<int>>>();
            string[] roleNames = { "Admin", "Professeur", "Etudiant" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }
        }
    }
}
