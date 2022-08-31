using Matchbox.Data;
using Matchbox.Models;
using Matchbox.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Matchbox.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MatchboxDBContext _context;

        private const string SessionUserID = "_UserID";
        private const string SessionRolAdmin = "_RolAdmin";

        public UsuariosController(MatchboxDBContext context)
        {
            _context = context;
        }

        // GET
        [Route("Registro")]
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Registro")]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Email,Contrasena,ReingresarContrasena")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.ReingresarContrasena != usuario.Contrasena)
                {
                    ModelState.AddModelError(nameof(usuario.ReingresarContrasena), "Las contraseñas no coinciden.");
                    return View(usuario);
                }

                Usuario newUser = new Usuario
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Contrasena = usuario.Contrasena,
                    ReingresarContrasena = usuario.ReingresarContrasena,
                    FechaAlta = DateTime.Now.Date,
                    FechaModificacion = DateTime.Now
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(usuario);
        }

        // GET: SignIn
        [Route("Ingreso")]
        public IActionResult SignIn()
        {
            return View();
        }

        // POST: SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ingreso")]
        public async Task<IActionResult> SignIn([Bind("Email,Contrasena")] LoginUserViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Usuario
                    .FirstOrDefaultAsync(m => m.Email == usuario.Email && m.Contrasena == usuario.Contrasena);

                if (user == null)
                {
                    ViewBag.GlobalError = "Usuario no encontrado. Verifique el email y la contraseña ingresados.";
                    return View(usuario);
                }

                HttpContext.Session.SetString(SessionUserID, user.Id.ToString());
                HttpContext.Session.SetString(SessionRolAdmin, "1");

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(usuario);
        }

        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}