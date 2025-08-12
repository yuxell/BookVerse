using System.ComponentModel.DataAnnotations.Schema;

namespace BookWebApp.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? CoverImage { get; set; } = "default-book.png";

        public int? UserID { get; set; } // Kitabı ekleyen kullanıcı (sadece admin olabilir)
        public AppUser? User { get; set; }

        public int? AuthorID { get; set; }
        public Author? Author { get; set; }

        public int? CategoryID { get; set; }
        public Category? Category { get; set; }

        public ICollection<BookPublisher>? BookPublishers { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<FavoriteBook>? FavoriteBooks { get; set; }

        //[NotMapped] // Veritabanına yansımaz, sadece kod tarafında hesaplanır. Bu yöntem sorgu maliyetini arttırır.
        //public double AverageRating => Reviews?.Count > 0 ? Reviews.Average(r => r.Rating) : 0;
        public double AverageRating { get; set; } // Yeni bir yorum eklendiğinde ortalama puan güncellenecek

        /*
         public async Task<IActionResult> UpdateBookRating(int bookId)
            {
                var book = await _bookRepository.GetByIdAsync(bookId);
                if (book != null)
                {
                    book.AverageRating = book.Reviews.Count > 0 ? book.Reviews.Average(r => r.Rating) : 0;
                    await _bookRepository.UpdateAsync(book);
                }
            }

        // Yorum eklendiğinde bunu çağır: 
        await UpdateBookRating(bookId);
         */



    }
}
