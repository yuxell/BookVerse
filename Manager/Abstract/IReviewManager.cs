using BookWebApp.Business.Utilities;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels.MyReviewsVM;
using BookWebApp.Models.ViewModels.Reviews;

namespace BookWebApp.Manager.Abstract
{
    public interface IReviewManager
    {
        Task<List<Review>>GetReviewsByBookIdAsync(int bookID);
        Task<Result> AddReview(Review review);
        Task<Result> DeleteReview(int reviewID);
        Task<Review> FindReviewAsync(int reviewID);
        Task<List<MyReviews_VM>> GetUsersReviews(int userID);
        Task<List<ReviewCard_VM>> GetRecentReviewsAsync(int takeCount);
    }
}
