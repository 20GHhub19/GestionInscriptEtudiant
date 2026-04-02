using GestionUnivApp;
using GestionUnivApp.Data;
using GestionUnivApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ==============================================
// --- Configuration de la base de données --- //
//===============================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<Utilisateur>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

// ==============================================
// --- Configuration de la partie Identity --- //
//===============================================

/*
 * builder.Services.AddIdentity<Utilisateur, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

*/

// ===================================================
// --- Cookies D'authentification personnalisee ---  //
//====================================================
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";
        options.LogoutPath = "/Identity/Account/Logout";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";

        options.Cookie.Name = "GestionUnivAuth";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });


// ====================================
// --- AUthentification par role ---  //
//=====================================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", 
        policy => policy.RequireRole("Administrateur"));
    
    options.AddPolicy("ProfesseurOnly", 
        policy => policy.RequireRole("Professeur"));

    options.AddPolicy("EtudiantOnlys", 
        policy => policy.RequireRole("Etudiant"));
});


// =======================
// --- MVC & RAZOR ---  //
//========================
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



var app = builder.Build();

// =============================================
// --- Configuration middleware pipeline. --- //
//=============================================

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


// ===================
// --- Routing. --- //
//====================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapStaticAssets();
app.MapRazorPages();
  // .WithStaticAssets();

// ======================
// --- SEED Roles. --- //
//=======================
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedRoles(services);
}*/
app.Run();
