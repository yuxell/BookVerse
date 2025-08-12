using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWebApp.Models.ViewModels.BookVM
{
    public class BookEditForm_VM
    {
        public BookEdit_VM Book { get; set; }
        public SelectList Author { get; set; }
        public SelectList Category { get; set; }
        public SelectList Publisher { get; set; }

        // Seçili yayınevleri için bir liste ekledik!
        //public List<int> SelectedPublishers { get; set; } = new(); // Seçili yoksa boş liste
    }
}
