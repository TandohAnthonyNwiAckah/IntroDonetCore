using Microsoft.AspNetCore.Mvc;

namespace IntroDonetCore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
