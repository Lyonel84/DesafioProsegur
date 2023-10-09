using Microsoft.AspNetCore.Mvc;

namespace WebProsegur.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
