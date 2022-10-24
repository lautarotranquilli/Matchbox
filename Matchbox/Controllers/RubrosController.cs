using Matchbox.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}