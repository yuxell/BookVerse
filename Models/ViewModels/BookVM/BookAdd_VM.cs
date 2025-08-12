using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models.ViewModels.BookVM
{
    public class BookAdd_VM
    {
        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat sıfırdan büyük olmalıdır.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Açıklama eklemelisiniz.")]
        [MinLength(10, ErrorMessage = "Açıklama en az 10 karakter olmalıdır.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Kapak görseli seçmelisiniz.")]
        public IFormFile CoverImage { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "Yazar seçmelisiniz.")]
        public int? AuthorID { get; set; }

        [Required(ErrorMessage = "Kategori seçmelisiniz.")]
        public int? CategoryID { get; set; }

        //[Required(ErrorMessage = "En az bir yayınevi seçmelisiniz.")]
        public List<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();

        [Required(ErrorMessage = "En az bir yayınevi seçmelisiniz.")]
        public List<int> SelectedPublishers { get; set; }
    }
}
