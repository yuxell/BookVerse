using BookWebApp.Manager.Abstract;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;

namespace BookWebApp.Manager.Concrete
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
