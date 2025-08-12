using Microsoft.AspNetCore.Identity;

namespace BookWebApp.Models
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Address { get; set; }


        // Kullanıcının favorilere eklediği kitaplar
        public ICollection<FavoriteBook>? FavoriteBooks { get; set; }

        // Kullanıcının yaptığı yorumlar
        public ICollection<Review>? Reviews{ get; set; }

        //// Sadece adminler için geçerli olacak: Eklediği kitaplar
        //public ICollection<Book>? Books { get; set; }
    }
}
