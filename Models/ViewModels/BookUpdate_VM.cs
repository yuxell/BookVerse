using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWebApp.Models.ViewModels
{
    public class BookUpdate_VM
    {
        public BookDetail_VM Book { get; set; }
        public List<SelectListItem> Author { get; set; }
        public SelectList Category { get; set; }
        public List<SelectListItem> Publisher { get; set; }

        // Seçili yayınevleri için bir liste ekledik!
        public List<int> SelectedPublisherIds { get; set; } = new(); // Seçili yoksa boş liste
    }
}
