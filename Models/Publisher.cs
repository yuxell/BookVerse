using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookWebApp.Models
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }
        public ICollection<BookPublisher>? BookPublishers { get; set; }
    }
}
