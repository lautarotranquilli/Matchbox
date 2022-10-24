using Matchbox.Data;
using Matchbox.Models;
using Matchbox.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Matchbox.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly MatchboxDBContext _context;

        public ServiciosController(MatchboxDBContext context)
        {
            _context = context;
        }

        // GET
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiciosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET
        [Route("Perfiles/Empresas/{empresaId}/Servicios/Nuevo")]
        public async Task<IActionResult> Create(int? empresaId)
        {
            if (HttpContext.Session.Get("_UserID") == null || empresaId == null)
                return NotFound();

            int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.IdUsuario == idUser && e.FechaBaja == null);

            if (empresa == null || empresa.Id != empresaId)
                return NotFound();

            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Perfiles/Empresas/{empresaId}/Servicios/Nuevo")]
        public async Task<IActionResult> Create(int? empresaId, [Bind("IdRubro,Nombre,Descripcion")] ServicioViewModel servicio)
        {
            if (empresaId == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

                Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.IdUsuario == idUser && e.FechaBaja == null);

                Servicio newServicio = new Servicio
                {
                    IdRubro = servicio.IdRubro,
                    IdEmpresa = empresa.Id,
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                    FechaAlta = DateTime.Today
                };

                _context.Add(newServicio);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Empresas", new { id = newServicio.IdEmpresa });
            }

            return View(servicio);
        }

        // GET
        [Route("Perfiles/Empresas/{empresaId}/Servicios/Editar/{id}")]
        public async Task<IActionResult> Edit(int? empresaId, int? id)
        {
            if (id == null 
                || empresaId == null
                || HttpContext.Session.Get("_UserID") == null)
                return RedirectToAction("Index", "Home");

            int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.IdUsuario == idUser && e.FechaBaja == null);

            if (empresa == null || empresa.Id != empresaId)
                return RedirectToAction("Index", "Home");

            Servicio servicio = await _context.Servicio.FirstOrDefaultAsync(s => s.Id == id && s.IdEmpresa == empresaId);

            if (servicio == null)
                return RedirectToAction("Details", "Empresas", new { id = empresaId });

            ServicioViewModel servicioVM = new ServicioViewModel
            {
                Id = servicio.Id,
                IdRubro = servicio.IdRubro,
                IdEmpresa = servicio.IdEmpresa,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                FechaAlta = servicio.FechaAlta
            };

            return View(servicioVM);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Perfiles/Empresas/{empresaId}/Servicios/Editar/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdRubro,IdEmpresa,Nombre,Descripcion,FechaAlta")] ServicioViewModel servicio)
        {
            if (id != servicio.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                Servicio editedServicio = new Servicio
                {
                    Id = servicio.Id,
                    IdRubro = servicio.IdRubro,
                    IdEmpresa = servicio.IdEmpresa,
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                    FechaAlta = servicio.FechaAlta,
                    FechaModificacion = DateTime.Now
                };

                _context.Update(editedServicio);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Empresas", new { id = editedServicio.IdEmpresa });
            }

            return View(servicio);
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