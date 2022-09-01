using Matchbox.Data;
using Matchbox.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
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

        // GET
        [Route("Perfiles/Cliente/Crear")]
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.Get("_UserID") == null)
            {
                return NotFound();
            }

            int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Cliente cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.IdUsuario == idUser);
            
            if (cliente != null)
            {
                return NotFound();
            }

            ViewBag.UserEmail = Encoding.Default.GetString(HttpContext.Session.Get("_UserEmail"));
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Perfiles/Cliente/Crear")]
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
                    IdUsuario = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID"))),
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                    IdProvincia = cliente.Provincia,
                    IdLocalidad = cliente.Localidad,
                    ProfilePath = uniqueFileName,
                    FechaAlta = DateTime.Now.Date,
                    FechaModificacion = DateTime.Now
                };

                _context.Add(newClient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(cliente);
        }

        // GET
        [Route("Perfiles/Cliente/Editar/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.Get("_UserID") == null)
            {
                return NotFound();
            }

            int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Cliente cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == id && c.IdUsuario == idUser);
            if (cliente == null)
            {
                return NotFound();
            }

            ClienteViewModel clientVM = new ClienteViewModel
            {
                Id = cliente.Id,
                IdUsuario = cliente.IdUsuario,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                Provincia = cliente.IdProvincia,
                Localidad = cliente.IdLocalidad,
                FotoPerfil = null,
                FotoPerfilPath = cliente.ProfilePath,
                FotoPerfilPath_Old = cliente.ProfilePath,
                FechaAlta = cliente.FechaAlta,
            };
            return View(clientVM);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Perfiles/Cliente/Editar/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,Nombre,Apellido,Telefono,Email,Provincia,Localidad,FotoPerfil,FotoPerfilPath,FechaAlta,FotoPerfilPath_Old")] ClienteViewModel cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Cliente newClient = new Cliente
                    {
                        Id = cliente.Id,
                        IdUsuario = cliente.IdUsuario,
                        Nombre = cliente.Nombre,
                        Apellido = cliente.Apellido,
                        Telefono = cliente.Telefono,
                        Email = cliente.Email,
                        IdProvincia = cliente.Provincia,
                        IdLocalidad = cliente.Localidad,
                        FechaAlta = cliente.FechaAlta,
                        FechaModificacion = DateTime.Now
                    };

                    if (cliente.FotoPerfilPath_Old != cliente.FotoPerfilPath)
                    {
                        string[] ss = new string[] { Directory.GetCurrentDirectory(), "wwwroot", "img", "user-profile", cliente.FotoPerfilPath_Old };
                        string path = Path.Combine(ss);

                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);

                        newClient.ProfilePath = null;
                    }

                    string uniqueFileName = null;

                    if (cliente.FotoPerfil != null)
                    {
                        uniqueFileName = UploadedFile(cliente);

                        if (uniqueFileName == "-1")
                        {
                            ModelState.AddModelError(nameof(cliente.FotoPerfil), "No se pudo cargar la imagen, inténtelo nuevamente");
                            return View(cliente);
                        }

                        newClient.ProfilePath = uniqueFileName;
                    }

                    _context.Update(newClient);
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
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(cliente);
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
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
    }
}