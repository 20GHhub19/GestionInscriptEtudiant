using System.Text.RegularExpressions;
using GestionUnivApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Identity/Account/Login";
        opt.LogoutPath = "/Identity/Account/Logout";
        opt.Cookie.Name = "GestionUnivAuth";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        opt.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly",      p => p.RequireRole("Administrateur"));
    opt.AddPolicy("ProfesseurOnly", p => p.RequireRole("Professeur"));
    opt.AddPolicy("EtudiantOnly",   p => p.RequireRole("Etudiant"));
});

builder.Services.AddRazorPages();

var app = builder.Build();

await ExecuterScriptsTSql(app);

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();


// Execute database/T-SQL.sql au demarrage (triggers, fonctions, vues).
// Idempotent : les scripts utilisent DROP IF EXISTS.
static async Task ExecuterScriptsTSql(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var sqlPath = Path.Combine(AppContext.BaseDirectory, "database", "T-SQL.sql");
    if (!File.Exists(sqlPath))
    {
        // Fallback dev : remonter de bin/Debug/netX vers la racine du repo.
        var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
        sqlPath = Path.Combine(repoRoot, "database", "T-SQL.sql");
    }

    if (!File.Exists(sqlPath))
    {
        app.Logger.LogWarning("T-SQL.sql introuvable - triggers/vues non crees.");
        return;
    }

    var script = await File.ReadAllTextAsync(sqlPath);
    var batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    foreach (var batch in batches)
    {
        if (string.IsNullOrWhiteSpace(batch)) continue;
        try { await db.Database.ExecuteSqlRawAsync(batch); }
        catch (Exception ex) { app.Logger.LogWarning(ex, "Echec d'un batch T-SQL au demarrage."); }
    }
    app.Logger.LogInformation("T-SQL.sql execute ({Count} batchs).", batches.Length);
}
