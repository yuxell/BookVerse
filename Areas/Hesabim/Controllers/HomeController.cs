using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Areas.Hesabim.Controllers
{
    [Area("Hesabim")]
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        
    }
}
