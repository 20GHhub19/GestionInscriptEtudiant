using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionUnivApp.Data;
using GestionUnivApp.Models.DTO;

namespace GestionUnivApp.ViewComponents;

public class MenuViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public MenuViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        // a- Charger TOUS les résultats UNE seule fois
        var lignes = _context.ProgrammesStructureRows
            .FromSqlRaw("EXEC Afficher_Programme_Spec_Cours")
            .ToList();

        // b-  Grouper par programme
        var model = lignes
            .GroupBy(l => new
            {
                l.IdProg,
                l.NomProg,
                l.CodeProg
            })
            .Select(programme => new MenuProgrammeBloc
            {
                IdProg = programme.Key.IdProg,
                NomProg = programme.Key.NomProg,
                CodeProg = programme.Key.CodeProg,

                Blocs = programme
                    .GroupBy(r => new { r.IdSpec, r.NomSpec })
                    .Select(spec => new BlocProgramme
                    {
                        IdSpec = spec.Key.IdSpec,
                        NomSpec = spec.Key.NomSpec,
                        Cours = spec.ToList()
                    })
                    .ToList()
            })
            .OrderBy(p => p.NomProg)
            .ToList();
        // var semestres = _context.Semestres.ToList();
        return View(model);
    }
}