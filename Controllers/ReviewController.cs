using BookWebApp.Manager.Abstract;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace BookWebApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewManager _reviewManager;

        public ReviewController(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        public IActionResult Index()
        {
            return Content("Review sayfası");
        }


        public async Task<IActionResult> RecentReviews(int reviewCount)
        {
            var result = await _reviewManager.GetRecentReviewsAsync(reviewCount);
            return Json(result);
        }
    }
}
