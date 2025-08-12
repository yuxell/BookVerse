using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        public int BookID { get; set; }
        public Book? Book { get; set; }

        public int UserID { get; set; } // MS Identity kullanıcısı
        public AppUser? User { get; set; }

        public string? Comment { get; set; } // Kullanıcı yorum yazmadan sadece puanlama yapabilir

        [Range(1, 5)]
        public int Rating { get; set; } // Milyonlarca kayıt olmayacağı için int tipinde kullanmak maliyeti yükseltmeyeceği için int kullanıldı
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
