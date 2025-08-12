using BookWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Kategori adı boş olamaz!")]
        [MaxLength(30, ErrorMessage = "Kategori adı en fazla 30 karakter olabilir.")]
        public string CategoryName { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}