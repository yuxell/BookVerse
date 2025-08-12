using System.ComponentModel.DataAnnotations.Schema;

namespace BookWebApp.Models
{
    public class FavoriteBook
    {
        public int FavoriteBookID { get; set; }


        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public AppUser? User { get; set; }


        public int BookID { get; set; }
        [ForeignKey("BookID")]
        public Book? Book { get; set; }
    }
}
