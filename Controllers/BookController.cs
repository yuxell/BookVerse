using BookWebApp.Helpers;
using BookWebApp.Manager.Abstract;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels;
using BookWebApp.Models.ViewModels.Reviews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IReviewManager _reviewManager;
        private readonly UserManager<AppUser> _userManager;

        public BookController(IBookManager bookManager, IReviewManager reviewManager, UserManager<AppUser> userManager)
        {
            _bookManager = bookManager;
            _reviewManager = reviewManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Kitaplar listelenecek

            return View();
        }

        /// <summary>
        /// Detaylı kitap, yorum ve publisherlar dahil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> BookDetail(int id)
        {
            if (id <= 0)
            {
                throw new Exception("HATA: Geçersiz id alındı!");
            }

            var bookDetail = await _bookManager.GetBooksByIDAsync(id);

            if (bookDetail == null)
            {
                throw new Exception("GetBooksByIDAsync metodu null döndü!");
            }
            if (bookDetail.BookPublishers == null)
            {
                throw new Exception("BookPublishers null geldi!");
            }
            if (!bookDetail.BookPublishers.Any())
            {
                return View(bookDetail);
            }

            return View(bookDetail);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreBooks(int page, int  pageSize = 10)
        {
            var books = await _bookManager.LoadMoreBooks(page, pageSize);
            //return Json(books);
            Console.WriteLine(books.ToString());
            return Json(books);
        }


        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review_VM review)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            var book = await _bookManager.GetBooksByIDAsync(review.BookID);
            if (book == null)
            {
                return NotFound("Kitap bulunamadı.");
            }

            // Yeni yorumu oluştur
            var newReview = new Review
            {
                BookID = review.BookID,
                UserID = user.Id,
                Comment = review.ReviewText,
                Rating = review.Rating,
                CreatedAt = DateTime.Now,
            };

            var reviewResult = await _reviewManager.AddReview(newReview);

            // Yeni puan ortalamasını hesapla
            var allReviews = await _reviewManager.GetReviewsByBookIdAsync(review.BookID);
            var newAvgRating = allReviews.Average(r => r.Rating);

            // Kitabın yeni puanını güncelle - Her yeni puan geldiğinde
            book.AverageRating = newAvgRating;
            await _bookManager.UpdateAverageRatingAsync(book.BookID, newAvgRating);
            
            return Json(new { success = true, newAverageRating = newAvgRating, newReviewID = reviewResult.ID });
        }

        // kullanıcı kendi yorumunu JS ile sildiğinde
        [HttpPost]
        public async Task<IActionResult> DeleteReview(int id)
        {
            //int id = data.ReviewID;
            var result = await _reviewManager.FindReviewAsync(id);

            if (result == null)
            {
                return new JsonResult(new { success = false, message = "Yorum bulunamadı." })
                {
                    ContentType = "application/json"
                };
            }

            await _reviewManager.DeleteReview(id);
            return new JsonResult(new { success = true, message = "Yorum başarıyla silindi." })
            {
                ContentType = "application/json"
            };

            return Json(new { success = false, message = "Bu yorumu silmeye yetkiniz yok." });
        }

        

    }
}
