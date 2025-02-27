using Microsoft.AspNetCore.Mvc;

namespace CombineMakerAI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
