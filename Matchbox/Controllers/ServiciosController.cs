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
    [Route("Servicios")]
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

        // GET
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            Servicio servicio = await _context.Servicio.FirstOrDefaultAsync(s => s.Id == id);

            if (servicio == null)
                return NotFound();

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.Id == servicio.IdEmpresa);

            Rubro rubro = await _context.Rubro.FirstOrDefaultAsync(r => r.Id == servicio.IdRubro);

            ServicioViewModel servicioVM = new ServicioViewModel
            {
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                eNombre = empresa.RazonSocial,
                rNombre = rubro.Nombre
            };

            return View(servicioVM);
        }

        // GET
        [Route("Nuevo")]
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.Get("_UserID") == null)
                return NotFound();

            int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.IdUsuario == idUser && e.FechaBaja == null);

            if (empresa == null)
                return NotFound();

            ServicioViewModel newServicio = new ServicioViewModel { IdEmpresa = empresa.Id };

            return View(newServicio);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Nuevo")]
        public async Task<IActionResult> Create([Bind("IdEmpresa, IdRubro,Nombre,Descripcion")] ServicioViewModel servicio)
        {
            if (ModelState.IsValid)
            {
                Servicio newServicio = new Servicio
                {
                    IdRubro = servicio.IdRubro,
                    IdEmpresa = servicio.IdEmpresa,
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
        [Route("Editar/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || HttpContext.Session.Get("_UserID") == null)
                return RedirectToAction("Index", "Home");

            int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.IdUsuario == idUser && e.FechaBaja == null);

            if (empresa == null)
                return RedirectToAction("Index", "Home");

            Servicio servicio = await _context.Servicio.FirstOrDefaultAsync(s => s.Id == id && s.IdEmpresa == empresa.Id);

            if (servicio == null)
                return RedirectToAction("Details", "Empresas", new { id = empresa.Id });

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
        [Route("Editar/{id}")]
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
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Servicio servicio = await _context.Servicio.FirstOrDefaultAsync(s => s.Id == id);

                servicio.FechaModificacion = DateTime.Now;
                servicio.FechaBaja = DateTime.Now.Date;

                _context.Update(servicio);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Empresas", new { id = servicio.IdEmpresa });
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }
    }
}