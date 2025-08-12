using BookWebApp.Business.Utilities;
using BookWebApp.Helpers;
using BookWebApp.Manager.Abstract;
using BookWebApp.Manager.Concrete;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels;
using BookWebApp.Models.ViewModels.BookVM;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;

namespace BookWebApp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BookController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IAuthorManager _authorManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IPublisherManager _publisherManager;
        private readonly UserManager<AppUser> _userManager;

        public BookController(IBookManager bookManager, IAuthorManager authorManager, ICategoryManager categoryManager, IPublisherManager publisherManager, UserManager<AppUser> userManager)
        {
            _bookManager = bookManager;
            _authorManager = authorManager;
            _categoryManager = categoryManager;
            _publisherManager = publisherManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            // yayında property si ekle!!!
            return View(await _bookManager.GetBookCardsAsync());
        }


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
                throw new Exception("BookPublishers boş liste olarak geldi!");
            }

            return View(bookDetail);
        }


        public async Task<IActionResult> Add()
        {
            BookAddForm_VM book = new BookAddForm_VM();
            var authors = await _authorManager.GetAllAuthorAsync();
            book.Author = new SelectList(authors.Select(a => new { a.AuthorID, FullName = a.AuthorName + " " + a.AuthorSurname }), "AuthorID", "FullName");

            var categories = await _categoryManager.GetAllCategoryAsync();
            book.Category = new SelectList(categories, "CategoryID", "CategoryName");

            var publishers = await _publisherManager.GetAllPublisherAsync();
            book.Publisher = new SelectList(publishers, "PublisherID", "PublisherName");

            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BookAdd_VM book)
        {
            string uploadedImage = "book.png";
            if(book.CoverImage != null)
                uploadedImage = Uploads.SaveImage(book.CoverImage);

            Book newBook = new Book
            {
                Title = book.Title,
                Price = book.Price,
                Description = book.Description,
                AuthorID = book.AuthorID,
                CategoryID = book.CategoryID,
                UserID = int.Parse(_userManager.GetUserId(User)),
                CoverImage = uploadedImage,
                BookPublishers = book.SelectedPublishers?.Select(publisherId => new BookPublisher
                {
                    PublisherID = publisherId
                }).ToList() ?? new List<BookPublisher>()
            };

            var result = await _bookManager.AddBookAsync(newBook);
            if (result.Success)
                return RedirectToAction("Index"); // İşlem başarılı ise listeye yönlendir

            else return View("404", result);            
        }


        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookManager.GetBooksByIDAsync(id);
            if (book == null) return NotFound();

            BookEditForm_VM bookForEdit = new BookEditForm_VM
            {
                Book = new BookEdit_VM
                {
                    BookID = book.BookID,
                    Title = book.Title,
                    Price = book.Price,
                    Description = book.Description,
                    CoverImage = book.CoverImage,
                    AuthorID = book.AuthorID,
                    AverageRating = book.AverageRating,
                    CategoryID = book.CategoryID
                }
            };
            var authors = await _authorManager.GetAllAuthorAsync();
            bookForEdit.Author = new SelectList( authors.Select(a=>new {a.AuthorID, FullName =a.AuthorName +" "+ a.AuthorSurname}), "AuthorID", "FullName", bookForEdit.Book.AuthorID);

            var categories = await _categoryManager.GetAllCategoryAsync();
            bookForEdit.Category = new SelectList(categories, "CategoryID", "CategoryName", bookForEdit.Book.CategoryID);

            var publishers = await _publisherManager.GetAllPublisherAsync();
            bookForEdit.Publisher = new SelectList(publishers, "PublisherID", "PublisherName");

            //Seçili olan Yayınevlerini atamak için ayrı liste gönderiyorum
            bookForEdit.Book.SelectedPublishers = book.BookPublishers.Select(bp => bp.PublisherID ?? 0).ToList();

            return View(bookForEdit);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(BookEditForm_VM bookForEdit)
        {
            Console.WriteLine($"__________________ KitapID: {bookForEdit.Book.BookID}  kategoriID: {bookForEdit.Book.CategoryID} YazarID: {bookForEdit.Book.AuthorID} Title: {bookForEdit.Book.Title} ");
            string uploadedImage = "";
            var book = await _bookManager.GetBooksByIDForUpdateAsync(bookForEdit.Book.BookID);
            
            if (book == null) return NotFound();

            book.Title = bookForEdit.Book.Title;
            book.Price = bookForEdit.Book.Price;
            book.Description = bookForEdit.Book.Description;
            book.AuthorID = bookForEdit.Book.AuthorID;
            book.CategoryID = bookForEdit.Book.CategoryID;
            book.UserID = int.Parse(_userManager.GetUserId(User));
            book.CoverImage = bookForEdit.Book.CoverImage;

            // Yayınevleri Güncelleme
            if(bookForEdit.Book.SelectedPublishers!=null)
            { 
                book.BookPublishers.Clear();
                foreach (var publisherId in bookForEdit.Book.SelectedPublishers)
                {
                    book.BookPublishers.Add(new BookPublisher { BookID = book.BookID, PublisherID = publisherId });
                }
            }
            
            var result = await _bookManager.UpdateBookAsync(book);
            if (result.Success)
                return RedirectToAction("Index"); // işlem başarılı ise

            else return View("404", result); // işlem hatalı ise 404'e gidiyor ve mesajı gösteriyor. 500 hatası için de yapılabilir
        }

        public async Task<IActionResult> DeleteReview(int id)
        {
            return Content(id.ToString());
        }

    }
}
