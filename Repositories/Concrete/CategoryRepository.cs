using BookWebApp.Data;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;

namespace BookWebApp.Repositories.Concrete
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}
