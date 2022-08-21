using Matchbox.Data;
using Matchbox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Matchbox.Controllers
{
    public class ClienteController : Controller
    {
        private readonly MatchboxDBContext _context;

        public ClienteController(MatchboxDBContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido,Telefono,Email,Provincia,Localidad,FotoPerfil")] ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(cliente);
                if (uniqueFileName == "-1")
                {
                    ModelState.AddModelError(nameof(cliente.FotoPerfil), "No se pudo cargar la imagen, inténtelo nuevamente");
                    return View(cliente);
                }

                Cliente newClient = new Cliente
                {
                    IdUsuario = 2,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                    Provincia = cliente.Provincia,
                    Localidad = cliente.Localidad,
                    ProfilePath = uniqueFileName,
                    FechaAlta = DateTime.Now.Date,
                    FechaModificacion = DateTime.Now
                };

                _context.Add(newClient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(cliente);
        }

        private string UploadedFile(ClienteViewModel cliente)
        {
            string uniqueFileName = null;

            if (cliente.FotoPerfil != null)
            {
                try
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "user-profile");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + cliente.FotoPerfil.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        cliente.FotoPerfil.CopyTo(fileStream);
                    }
                }
                catch (Exception)
                {
                    uniqueFileName = "-1";
                }
            }
            return uniqueFileName;
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,Nombre,Apellido,Telefono,Email,Provincia,Localidad,FechaAlta")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cliente.FechaModificacion = DateTime.Now;
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            try
            {
                cliente.FechaModificacion = DateTime.Now;
                cliente.FechaBaja = DateTime.Now.Date;
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}