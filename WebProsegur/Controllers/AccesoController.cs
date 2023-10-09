using Microsoft.AspNetCore.Mvc;

namespace WebProsegur.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }
    }
}
