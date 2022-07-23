using Microsoft.AspNetCore.Mvc;

namespace PS4GamingApplication.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
