using BookWebApp.Data;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;

namespace BookWebApp.Repositories.Concrete
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context) : base(context) { }
    }
}
