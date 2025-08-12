using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWebApp.Models.ViewModels.BookVM
{
    public class BookAddForm_VM
    {
        public BookAdd_VM Book { get; set; }
        public SelectList Author { get; set; }
        public SelectList Category { get; set; }
        public SelectList Publisher { get; set; }
    }
}
