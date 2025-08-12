namespace BookWebApp.Models.ViewModels.BookVM
{
    public class BookEdit_VM
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; } = "default-book.png";

        public int? AuthorID { get; set; }

        public int? CategoryID { get; set; }
        public string Category { get; set; }
        public List<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
        public double AverageRating { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<int> SelectedPublishers { get; set; }
    }
}
