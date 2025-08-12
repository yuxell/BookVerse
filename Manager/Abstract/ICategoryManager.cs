using BookWebApp.Models;

namespace BookWebApp.Manager.Abstract
{
    public interface ICategoryManager
    {
        Task<List<Category>> GetAllCategoryAsync();
    }
}
