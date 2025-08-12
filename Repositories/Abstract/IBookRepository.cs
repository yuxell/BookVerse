using BookWebApp.Business.Utilities;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels;

namespace BookWebApp.Repositories.Abstract
{
    public interface IBookRepository : IBaseRepository<Book>
    {

        Task<Result> UpdateBookAsync(Book book);
        Task UpdateAverageRatingAsync(int bookId, double newAverageRating);

        Task<int> GetBookCountAsync();
    }
}
