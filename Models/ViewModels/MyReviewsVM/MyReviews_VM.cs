using System.ComponentModel.DataAnnotations;
using BookWebApp.Models.ViewModels.MyReviewsVM;

namespace BookWebApp.Models.ViewModels.MyReviewsVM
{
    public class MyReviews_VM
    {
        public int ReviewID { get; set; }
        public int BookID { get; set; }
        public Book? Book { get; set; }

        public int UserID { get; set; }
        public AppUser? User { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
