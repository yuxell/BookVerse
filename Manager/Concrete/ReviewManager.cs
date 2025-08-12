using BookWebApp.Business.Utilities;
using BookWebApp.Manager.Abstract;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels.MyReviewsVM;
using BookWebApp.Models.ViewModels.Reviews;
using BookWebApp.Repositories.Abstract;
using BookWebApp.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Manager.Concrete
{
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewManager(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Result> AddReview(Review review)
        {
            return await _reviewRepository.AddAsync(review);
        }

        public async Task<Result> DeleteReview(int reviewID)
        {
            return await _reviewRepository.DeleteAsync(reviewID);
        }

        public async Task<List<Review>> GetReviewsByBookIdAsync(int bookID)
        {
            return await _reviewRepository.GetReviewsByBookIdAsync(bookID);
        }
        public async Task<Review> FindReviewAsync(int reviewID)
        {
            return await _reviewRepository.GetByIDAsync(reviewID);
        }

        public async Task<List<MyReviews_VM>> GetUsersReviews(int userID)
        {
            var result = await _reviewRepository.FilterAllAsync(
                select: review => new MyReviews_VM
                {
                    ReviewID = review.ReviewID,
                    BookID = review.BookID,
                    Book = review.Book,
                    UserID = review.UserID,
                    User = review.User,
                    Comment = review.Comment,
                    Rating = review.Rating,
                    CreatedAt = review.CreatedAt
                },
                where: review => review.UserID == userID,
                include: query => query.Include(r => r.Book).Include(r => r.User),
                orderBy: query => query.OrderByDescending(r => r.CreatedAt)
                );
            return result.ToList();
            
        }

        public async Task<List<ReviewCard_VM>> GetRecentReviewsAsync(int takeCount)
        {
            return await _reviewRepository.GetRecentReviewsAsync(takeCount);
        }
    }
}
