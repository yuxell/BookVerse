using BookWebApp.Manager.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        private readonly IBookManager _bookManager;

        public HomeController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _bookManager.GetBookCardsAsync());
        }
    }
}
