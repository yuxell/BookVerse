namespace BookWebApp.Models
{
    public class BookPublisher
    {
        public int BookPublisherID { get; set; }

        public int? BookID { get; set; }
        public Book? Book { get; set; }

        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
