using BookWebApp.Business.Utilities;
using BookWebApp.Data;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels;
using BookWebApp.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Repositories.Concrete
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<Result> UpdateBookAsync(Book book)
        {
            if (book != null)
            {
                // Mevcut BookPublishers kayıtlarını sil
                var existingPublishers = _context.BookPublishers.Where(bp => bp.BookID == book.BookID);
                _context.BookPublishers.RemoveRange(existingPublishers);

                // Yeni yayınevleri ilişkilerini ekle
                
                if (book.BookPublishers != null && book.BookPublishers.Any())
                {
                    _context.BookPublishers.AddRange(book.BookPublishers);
                }

                return await UpdateAsync(book);
            }
            return Result.Fail("Bir hata oluştu");
        }

        // kitabın sadece puanını günceller
        public async Task UpdateAverageRatingAsync(int bookId, double newAverageRating)
        {
            var book = new Book { BookID = bookId, AverageRating = newAverageRating };
            _context.Books.Attach(book);
            _context.Entry(book).Property(b => b.AverageRating).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetBookCountAsync()
        {
            var result = await _context.Books.CountAsync();
            Console.WriteLine(result+"----------------------------------------------------");
            return result;
        }
    }
}