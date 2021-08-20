
using Microsoft.AspNetCore.Mvc;


namespace SHS.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}