using BookWebApp.Business.Utilities;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels.Reviews;

namespace BookWebApp.Repositories.Abstract
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<List<Review>> GetReviewsByBookIdAsync(int bookID);
        Task<List<ReviewCard_VM>> GetRecentReviewsAsync(int take);
    }
}
