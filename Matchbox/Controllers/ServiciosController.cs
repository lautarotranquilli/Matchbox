using Matchbox.Data;
using Matchbox.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Matchbox.Controllers
{
    [Route("Servicios")]
    public class ServiciosController : Controller
    {
        private readonly MatchboxDBContext _context;

        public ServiciosController(MatchboxDBContext context)
        {
            _context = context;
        }

        // GET: ServiciosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiciosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiciosController/Create
        [Route("Crear")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ServiciosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Crear")]
        public async Task<IActionResult> Create([Bind("IdRubro,Nombre,Descripcion")] Servicio servicio)
        {
            try
            {
                servicio.IdEmpresa = 2;
                servicio.FechaAlta = System.DateTime.Today;

                _context.Add(servicio);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiciosController/Edit/5
        [Route("Editar/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Servicio servicio = await _context.Servicio.FirstOrDefaultAsync(s => s.Id == id);

            return View(servicio);
        }

        // POST: ServiciosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdRubro,IdEmpresa,Nombre,Descripcion,FechaAlta")] Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return NotFound();
            }

            try
            {
                servicio.FechaModificacion = System.DateTime.Now;

                _context.Update(servicio);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiciosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: ServiciosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
