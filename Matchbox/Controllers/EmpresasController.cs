using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Matchbox.Data;
using Matchbox.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;

namespace Matchbox.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly MatchboxDBContext _context;

        public EmpresasController(MatchboxDBContext context)
        {
            _context = context;
        }

        // GET
        [Route("Perfiles/Empresas/Crear")]
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.Get("_UserID") == null)
            {
                return NotFound();
            }

            int idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(c => c.IdUsuario == idUser);

            if (empresa != null)
            {
                return NotFound();
            }

            ViewBag.UserEmail = Encoding.Default.GetString(HttpContext.Session.Get("_UserEmail"));
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Perfiles/Empresas/Crear")]
        public async Task<IActionResult> Create([Bind("IdUsuario,RazonSocial,Telefono,Email,Provincia,Localidad,FotoPerfil")] EmpresaViewModel empresa)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(empresa);
                if (uniqueFileName == "-1")
                {
                    ModelState.AddModelError(nameof(empresa.FotoPerfil), "No se pudo cargar la imagen, inténtelo nuevamente");
                    return View(empresa);
                }

                Empresa newEmpresa = new Empresa
                {
                    IdUsuario = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID"))),
                    RazonSocial = empresa.RazonSocial,
                    Telefono = empresa.Telefono,
                    Email = empresa.Email,
                    IdProvincia = empresa.Provincia,
                    IdLocalidad = empresa.Localidad,
                    ProfilePath = uniqueFileName,
                    FechaAlta = DateTime.Now.Date,
                    FechaModificacion = DateTime.Now
                };

                _context.Add(newEmpresa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });

            }

            ViewBag.UserEmail = Encoding.Default.GetString(HttpContext.Session.Get("_UserEmail"));
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        [Route("Perfiles/Empresas/Editar/{id}")]
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

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(c => c.Id == id && c.IdUsuario == idUser);
            if (empresa == null)
            {
                return NotFound();
            }

            EmpresaViewModel empresaVM = new EmpresaViewModel
            {
                Id = empresa.Id,
                IdUsuario = empresa.IdUsuario,
                RazonSocial = empresa.RazonSocial,
                Telefono = empresa.Telefono,
                Email = empresa.Email,
                Provincia = empresa.IdProvincia,
                Localidad = empresa.IdLocalidad,
                FotoPerfil = null,
                FotoPerfilPath = empresa.ProfilePath,
                FotoPerfilPath_Old = empresa.ProfilePath,
                FechaAlta = empresa.FechaAlta,
            };

            ViewBag.UserEmail = Encoding.Default.GetString(HttpContext.Session.Get("_UserEmail"));
            return View(empresaVM);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Perfiles/Empresas/Editar/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,RazonSocial,Telefono,Email,Provincia,Localidad,FotoPerfil,FotoPerfilPath,FechaAlta,FotoPerfilPath_Old")] EmpresaViewModel empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Empresa newEmpresa = new Empresa
                    {
                        Id = empresa.Id,
                        IdUsuario = empresa.IdUsuario,
                        RazonSocial = empresa.RazonSocial,
                        Telefono = empresa.Telefono,
                        Email = empresa.Email,
                        IdProvincia = empresa.Provincia,
                        IdLocalidad = empresa.Localidad,
                        FechaAlta = empresa.FechaAlta,
                        FechaModificacion = DateTime.Now
                    };

                    if (empresa.FotoPerfilPath_Old != empresa.FotoPerfilPath)
                    {
                        string[] ss = new string[] { Directory.GetCurrentDirectory(), "wwwroot", "img", "user-profile", empresa.FotoPerfilPath_Old };
                        string path = Path.Combine(ss);

                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);

                        newEmpresa.ProfilePath = null;
                    }

                    string uniqueFileName = null;

                    if (empresa.FotoPerfil != null)
                    {
                        uniqueFileName = UploadedFile(empresa);

                        if (uniqueFileName == "-1")
                        {
                            ModelState.AddModelError(nameof(empresa.FotoPerfil), "No se pudo cargar la imagen, inténtelo nuevamente");
                            return View(empresa);
                        }

                        newEmpresa.ProfilePath = uniqueFileName;
                    }

                    _context.Update(newEmpresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            ViewBag.UserEmail = Encoding.Default.GetString(HttpContext.Session.Get("_UserEmail"));
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresa.FindAsync(id);
            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresa.Any(e => e.Id == id);
        }

        private string UploadedFile(EmpresaViewModel empresa)
        {
            string uniqueFileName = null;

            if (empresa.FotoPerfil != null)
            {
                try
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "user-profile");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + empresa.FotoPerfil.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        empresa.FotoPerfil.CopyTo(fileStream);
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
