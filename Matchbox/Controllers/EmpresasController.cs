using Matchbox.Data;
using Matchbox.Models;
using Matchbox.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.IdUsuario == idUser && e.FechaBaja == null);

            if (empresa != null)
            {
                return RedirectToAction("Details", new { id = idUser });
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

        // GET
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
            bool isAdmin = HttpContext.Session.GetString("_UserAdmin") == "1";
            Empresa empresa;

            if (isAdmin)
                empresa = await _context.Empresa.FirstOrDefaultAsync(c => c.Id == id);
            else
                empresa = await _context.Empresa.FirstOrDefaultAsync(c => c.Id == id && c.IdUsuario == idUser);

            if (empresa == null || empresa.FechaBaja != null)
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
            ViewBag.FromAdmin = HttpContext.Session.GetString("_UserAdmin") == "1";
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
                if (HttpContext.Session.GetString("_UserAdmin") == "1")
                    return RedirectToAction("Index", "Usuarios", new { area = "" });
                else
                    return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.UserEmail = Encoding.Default.GetString(HttpContext.Session.Get("_UserEmail"));
            ViewBag.FromAdmin = HttpContext.Session.GetString("_UserAdmin") == "1";
            return View(empresa);
        }

        [HttpGet]
        [Route("Perfiles/Empresas/Detalles/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            if (id == 0)
                return RedirectToAction("Create");

            int? idUser = 0;
            if (HttpContext.Session.Get("_UserID") != null)
                idUser = Convert.ToInt32(Encoding.Default.GetString(HttpContext.Session.Get("_UserID")));

            Empresa empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.IdUsuario == id && e.FechaBaja == null);

            if (empresa == null)
                return RedirectToAction("Create");

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

            var servicios = await _context.Servicio
                                            .Join(inner: _context.Rubro,
                                                  outerKeySelector: ser => ser.IdRubro,
                                                  innerKeySelector: rub => rub.Id,
                                                  resultSelector: (ser, rub) => new ServiciosListViewModel
                                                  {
                                                      sId = ser.Id,
                                                      sNombre = ser.Nombre,
                                                      rNombre = rub.Nombre,
                                                      sFechaBaja = ser.FechaBaja,
                                                      sEmpresa = ser.IdEmpresa
                                                  })
                                            .Where(x => x.sEmpresa == empresaVM.Id && x.sFechaBaja == null)
                                            .ToListAsync();

            if (servicios != null)
            {
                empresaVM.ServiciosList = new List<ServiciosListViewModel>();
                servicios.ForEach(s => empresaVM.ServiciosList.Add(s));
            }
            //empresaVM.ServiciosList = servicios;

            ViewBag.ShowOptions = HttpContext.Session.GetString("_UserAdmin") == "1" || empresa.IdUsuario == idUser;
            return View(empresaVM);
        }

        // POST: Empresas/Delete/5
        [HttpGet]
        [Route("Perfiles/Empresas/Eliminar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var empresa = await _context.Empresa.FirstOrDefaultAsync(c => c.Id == id);

                empresa.FechaModificacion = DateTime.Now;
                empresa.FechaBaja = DateTime.Now.Date;

                _context.Update(empresa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            //return RedirectToAction("Index", "Cliente", new { area = "" });
            return View("DeleteConfirm");
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