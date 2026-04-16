using GestionUnivApp.Models.ViewModels;
using GestionUnivApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GestionUnivApp.Pages.Programmes
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProgrammeDetailsVM Programme { get; set; }

        public void OnGet(int id)
        {
            var lignes = _context.ProgrammeDetailsRows
                .FromSqlRaw("EXEC Programme_Detail @IdProg",
                    new SqlParameter("@IdProg", id))
                .ToList();

            if (!lignes.Any())
                return;

            var first = lignes.First();

            Programme = new ProgrammeDetailsVM
            {
                IdProg = first.IdProg,
                NomProg = first.NomProg,
                CodeProg = first.CodeProg,
                DateCreatProg = first.DateCreatProg,
                DureeProg = first.DureeProg,
                NbCreditProg = first.NbCreditProg,
                UniteProg = first.UniteProg,
                DescriptProg = first.DescriptProg
            };

            Programme.Specialisations = lignes
                .GroupBy(l => new { l.IdSpec, l.NomSpec, l.NbCreditSpec, l.DescriptSpec })
                .Select(g => new SpecialisationVM
                {
                    IdSpec = g.Key.IdSpec,
                    NomSpec = g.Key.NomSpec,
                    NbCreditSpec = g.Key.NbCreditSpec,
                    DescriptSpec = g.Key.DescriptSpec,

                    Cours = g.Where(x => x.IdCours != null)
                             .Select(x => new CoursVM
                             {
                                 IdCours = x.IdCours.Value,
                                 NomCours = x.NomCours,
                                 CodeCours = x.CodeCours,
                                 NbHeureCours = x.NbHeureCours,
                                 CreditCours = x.CreditCours,
                                 TypeCours = x.TypeCours
                             }).ToList()
                })
                .ToList();
        }
    }
}
