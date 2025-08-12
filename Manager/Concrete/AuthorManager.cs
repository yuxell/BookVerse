using BookWebApp.Manager.Abstract;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;

namespace BookWebApp.Manager.Concrete
{
    public class AuthorManager : IAuthorManager
    {
        public readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAuthorAsync()
        {
            return await _authorRepository.GetAllAsync();
        }
    }
}
