using BookWebApp.Data;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;

namespace BookWebApp.Repositories.Concrete
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context) { }
    }
}
