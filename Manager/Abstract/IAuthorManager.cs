using BookWebApp.Models;
using BookWebApp.Models.ViewModels;

namespace BookWebApp.Manager.Abstract
{
    public interface IAuthorManager
    {
        Task<List<Author>> GetAllAuthorAsync();
    }
}
