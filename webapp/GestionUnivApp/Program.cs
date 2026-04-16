using System.Text.RegularExpressions;
using GestionUnivApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
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

await InitialiserBaseDeDonnees(app);
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


// Execute database/LDD_LMD.sql au demarrage si la base n'existe pas encore.
static async Task InitialiserBaseDeDonnees(WebApplication app)
{
    var connStr = app.Configuration.GetConnectionString("DefaultConnection")!;
    var connBuilder = new SqlConnectionStringBuilder(connStr);
    var dbName = connBuilder.InitialCatalog;

    // Chercher LDD_LMD.sql
    var sqlPath = Path.Combine(AppContext.BaseDirectory, "database", "LDD_LMD.sql");
    if (!File.Exists(sqlPath))
    {
        var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
        sqlPath = Path.Combine(repoRoot, "database", "LDD_LMD.sql");
    }
    if (!File.Exists(sqlPath))
    {
        app.Logger.LogWarning("LDD_LMD.sql introuvable — initialisation ignoree.");
        return;
    }

    // 1. Creer la base de donnees si elle n'existe pas (connexion a master)
    var masterConnStr = new SqlConnectionStringBuilder(connStr) { InitialCatalog = "master" }.ConnectionString;
    using (var conn = new SqlConnection(masterConnStr))
    {
        await conn.OpenAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $"""
            IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = '{dbName}')
                CREATE DATABASE [{dbName}];
            """;
        await cmd.ExecuteNonQueryAsync();
    }

    // 2. Verifier si les tables existent deja
    using (var conn = new SqlConnection(connStr))
    {
        await conn.OpenAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT OBJECT_ID('Programme', 'U')";
        var result = await cmd.ExecuteScalarAsync();
        if (result is not null and not DBNull)
        {
            app.Logger.LogInformation("Base de donnees deja initialisee.");
            return;
        }
    }

    // 3. Executer les batches de creation de tables et d'insertion
    var script = await File.ReadAllTextAsync(sqlPath);
    var batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    using (var conn = new SqlConnection(connStr))
    {
        await conn.OpenAsync();
        foreach (var batch in batches)
        {
            // Retirer les instructions USE (on est deja connecte a la bonne BD)
            var sql = Regex.Replace(batch, @"^\s*USE\s+\w+\s*;?\s*$", "", RegexOptions.Multiline | RegexOptions.IgnoreCase).Trim();
            if (string.IsNullOrWhiteSpace(sql)) continue;
            if (Regex.IsMatch(sql, @"CREATE\s+DATABASE", RegexOptions.IgnoreCase)) continue;

            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                app.Logger.LogWarning(ex, "Echec d'un batch LDD_LMD au demarrage.");
            }
        }
    }

    app.Logger.LogInformation("LDD_LMD.sql execute — base de donnees initialisee.");
}

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
