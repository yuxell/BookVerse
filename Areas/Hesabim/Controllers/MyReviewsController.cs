using BookWebApp.Manager.Abstract;
using BookWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Areas.Hesabim.Controllers
{
    [Area("Hesabim")]
    [Authorize(Roles = "User")]
    public class MyReviewsController : Controller
    {
        private readonly IReviewManager _reviewManager;
        private readonly UserManager<AppUser> _userManager;

        public MyReviewsController(IReviewManager reviewManager, UserManager<AppUser> userManager)
        {
            _reviewManager = reviewManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _reviewManager.GetUsersReviews(int.Parse(_userManager.GetUserId(User))));
        }
    }
}
