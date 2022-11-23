using Matchbox.Data;
using Matchbox.Models;
using Matchbox.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matchbox.Controllers
{
    [Route("Rubros")]
    public class RubrosController : Controller
    {
        private readonly MatchboxDBContext _context;

        public RubrosController(MatchboxDBContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> GetList()
        {
            var response = await _context.Rubro.Where(r => r.FechaBaja == null).ToArrayAsync();

            return Ok(response);
        }

        [Route("RubrosList")]
        public async Task<IActionResult> RubrosList()
        {
            List<RubrosViewModel> rubrosToList = new List<RubrosViewModel>();
            var rubros = await _context.Rubro.ToArrayAsync();

            foreach (var rubro in rubros)
            {

                rubrosToList.Add(new RubrosViewModel
                {
                    Id = rubro.Id,
                    Nombre = rubro.Nombre,
                }) ;
            }

            return View(rubrosToList);
        }
           
    }
}