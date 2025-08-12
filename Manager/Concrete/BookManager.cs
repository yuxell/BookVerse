using BookWebApp.Business.Utilities;
using BookWebApp.Manager.Abstract;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels;
using BookWebApp.Models.ViewModels.BookVM;
using BookWebApp.Repositories.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookWebApp.Manager.Concrete
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result> AddBookAsync(Book book)
        {
            var result = await _bookRepository.AddAsync(book);
            return result;
        }

        public async Task<List<BookCard_VM>> GetBookCardsAsync()
        {
            var result = await _bookRepository.FilterAllAsync(
                select: b => new BookCard_VM
                {
                    BookID = b.BookID,
                    Title = b.Title,
                    AuthorFullName = b.Author.AuthorName + (string.IsNullOrEmpty(b.Author.AuthorSurname) ? "" : " " + b.Author.AuthorSurname),
                    AuthorID = b.AuthorID,
                    CoverImage = b.CoverImage,
                    Price = b.Price,
                    CategoryID = b.Category.CategoryID,
                    CategoriName = b.Category.CategoryName,
                    Description = b.Description,
                    AvarageRating = b.AverageRating
                },
                where: b => true,
                include: b => b.Include(b => b.Author)
                               .Include(b => b.Category),
                orderBy: null
                );
            return result.ToList();
        }

        public async Task<List<BookCard_VM>> GetBookCardsFuturedAsync(int takeCount)
        {
            var result = await _bookRepository.FilterAllAsync(
                select: b => new BookCard_VM
                {
                    BookID = b.BookID,
                    Title = b.Title,
                    AuthorFullName = b.Author.AuthorName + (string.IsNullOrEmpty(b.Author.AuthorSurname) ? "" : " " + b.Author.AuthorSurname),
                    AuthorID = b.AuthorID,
                    CoverImage = b.CoverImage,
                    Price = b.Price,
                    CategoriName = b.Category.CategoryName,
                    CategoryID = b.Category.CategoryID,
                    Description = b.Description,
                    AvarageRating = b.AverageRating
                },
                where: b => true,
                include: b => b.Include(b => b.Author)
                                .Include(b => b.Category),
                orderBy: b => b.OrderBy(b => b.BookID),
                take: takeCount
                );
            return result.ToList();
        }

        [HttpGet]
        public async Task<List<BookCard_VM>> LoadMoreBooks(int page = 1, int pageSize = 10)
        {
            var booksList = await GetBookCardsAsync();

            var booksQuery = booksList.AsQueryable(); // List -> IQueryable dönüşümü, sayfalamada kullanmak üzere gerekli metodları kullanabilmek için

            var result = booksQuery
                .OrderBy(b => b.BookID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookCard_VM
                {
                    BookID = b.BookID,
                    Title = b.Title,
                    AuthorFullName = b.AuthorFullName,
                    AuthorID = b.AuthorID,
                    Price = b.Price,
                    CoverImage = b.CoverImage
                }).ToList();

            return result;

        }
        public Task<List<Book>> GetBooksByAuthorAsync(int author)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        

        public Task<List<Book>> SearchBooksAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task<BookDetail_VM> GetBooksByIDAsync(int bookID)
        {
            var result = await _bookRepository.FilterAllAsync(
                select: b => new BookDetail_VM
                {
                    BookID = b.BookID,
                    Title = b.Title,
                    AuthorFullName = b.Author.AuthorName + (string.IsNullOrEmpty(b.Author.AuthorSurname) ? "" : " " + b.Author.AuthorSurname),
                    AuthorID = b.AuthorID,
                    CoverImage = b.CoverImage,
                    Price = b.Price,
                    CategoryID = b.CategoryID,
                    Category = b.Category.CategoryName,
                    Description = b.Description,
                    AverageRating = b.AverageRating,

                    // Kitaba bağlı tüm yayınevlerini listeye çevir
                    BookPublishers = b.BookPublishers.ToList(),

                    // Kitaba bağlı tüm yorumları listeye çevir
                    Reviews = b.Reviews.ToList()
                },
                where: b => b.BookID == bookID, // Filtreleme burada
                orderBy: null,
                include: b => b
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .Include(b => b.BookPublishers).ThenInclude(bp => bp.Publisher)
                    .Include(b => b.Reviews).ThenInclude(r => r.User) // Yorumu yazan kullanıcıyı getir


            );

            if (result == null)
            {
                throw new Exception($"Veritabanında ID {bookID} olan kitap bulunamadı!");
            }
            return result.FirstOrDefault();
        }




        // Update için
        public async Task<Book> GetBooksByIDForUpdateAsync(int bookID)
        {
            var result = await _bookRepository.FilterAllAsync(
                select: b => new Book
                {
                    BookID = b.BookID,
                    Title = b.Title,
                    AuthorID = b.AuthorID,
                    CoverImage = b.CoverImage,
                    Price = b.Price,
                    CategoryID = b.CategoryID,
                    Description = b.Description,
                    AverageRating = b.AverageRating,

                    // Kitaba bağlı tüm yayınevlerini listeye çevir
                    BookPublishers = b.BookPublishers.ToList(),

                    // Kitaba bağlı tüm yorumları listeye çevir
                    Reviews = b.Reviews.ToList()
                },
                where: b => b.BookID == bookID, // Filtreleme burada
                orderBy: null,
                include: b => b
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .Include(b => b.BookPublishers).ThenInclude(bp => bp.Publisher)
                    .Include(b => b.Reviews).ThenInclude(r => r.User) // Yorumu yazan kullanıcıyı getir


                );

            if (result == null)
            {
                throw new Exception($"Veritabanında ID {bookID} olan kitap bulunamadı!");
            }
            return result.FirstOrDefault();
        }

        public async Task<Result> UpdateBookAsync(Book book)
        {
            // kalkacak: metodu direkt return içine al
            var result = await _bookRepository.UpdateBookAsync(book);

            return result;
                
        }

        // Repo'ya eklenecek
        public async Task UpdateAverageRatingAsync(int bookId, double newAverageRating)
        {
            await _bookRepository.UpdateAverageRatingAsync(bookId, newAverageRating);
        }

        public async Task<int> GetBookCountAsync()
        {
            return await _bookRepository.GetBookCountAsync();
        }
    }
}
