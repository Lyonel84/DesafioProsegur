using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProsegur.Models;

namespace WebProsegur.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string name, int idrol)
        {
            ViewData["Name"] = name;
            ViewData["IdRol"] = idrol;
            ViewData["Rol"] = idrol== 4? "Usuario": ( idrol == 3 ? "Empledo": (idrol == 2 ? "Supervisor": "Administrador"));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}