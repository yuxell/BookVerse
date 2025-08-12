namespace BookWebApp.Models.ViewModels
{
    public class BookCard_VM
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? CoverImage { get; set; }
        public int? AuthorID { get; set; }
        public string AuthorFullName { get; set; }
        public int CategoryID { get; set; }
        public string CategoriName { get; set; }
        public string Description { get; set; }
        public double AvarageRating { get; set; }

    }
}
