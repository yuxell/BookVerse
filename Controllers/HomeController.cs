using System.Diagnostics;
using BookWebApp.Models;
using BookWebApp.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc;
using BookWebApp.Manager.Abstract;
using BookWebApp.Repositories.Abstract;

namespace BookWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IReviewManager _reviewManager;

        public HomeController(IBookManager bookManager, IReviewManager reviewManager)
        {
            _bookManager = bookManager;
            _reviewManager = reviewManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookManager.GetBookCardsFuturedAsync(4);
            
            return View(result);
        }

        public async Task<IActionResult> JSBooks()
        {
            var books = await _bookManager.GetBookCardsAsync();
            return Json(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Error(int? code)
        {
            if (code == 404)
            {
                ViewData["ErrorMessage"] = "Arad���n�z sayfa bulunamad�.";
                ViewData["ErrorCode"] = 404;
                return View("404"); // Views/Shared/404.cshtml a��lacak
            }
            else if (code == 500)
            {
                ViewData["ErrorMessage"] = "Sunucu hatas� meydana geldi.";
                ViewData["ErrorCode"] = 500;
                return View("Error"); // Views/Shared/Error.cshtml a��lacak
            }

            // Di�er hatalar i�in genel hata sayfas�
            ViewData["ErrorMessage"] = "Bilinmeyen bir hata olu�tu.";
            ViewData["ErrorCode"] = code ?? 0;
            return View("Error");
        }

        //[HttpGet("allBookCount")]
        public async Task<IActionResult> GetBookCount()
        {
            int count = await _bookManager.GetBookCountAsync();
            return Ok(count);
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }
        public IActionResult  NotFound()
        {
            return View();
        }
    }
}



// 