
using Microsoft.AspNetCore.Mvc;


namespace SHS.Controllers
{
    /// <summary>
    /// Controller that provide the home page.
    /// </summary>
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        /// <summary>
        /// Display home page.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}