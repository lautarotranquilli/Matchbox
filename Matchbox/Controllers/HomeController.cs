using Matchbox.Data;
using Matchbox.Models;
using Matchbox.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Matchbox.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly MatchboxDBContext _context;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(MatchboxDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<ServicioViewModel> servicioToList = new List<ServicioViewModel>();
            var servicios = await _context.Servicio.ToArrayAsync();

            foreach (var servicio in servicios)
            {

                servicioToList.Add(new ServicioViewModel
                {
                    Id = servicio.Id,
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                });
            }

            return View(servicioToList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Admin")]
        public IActionResult Admin()
        {
            if (HttpContext.Session.GetString("_UserAdmin") != "1")
                return RedirectToAction("Index", "Home", new { area = "" });

            return View();
        }
    }
}
