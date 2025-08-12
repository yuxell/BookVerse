using BookWebApp.Business.Utilities;
using BookWebApp.Data;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels.Reviews;
using BookWebApp.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Repositories.Concrete
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context) { }

        public async Task<List<Review>> GetReviewsByBookIdAsync(int bookID)
        {
            return await _tables.Where(r => r.BookID == bookID).ToListAsync();
        }

        //public async Task<Result> AddReviewAsync(Review review)
        //{
        //    await _tables.AddAsync(review);
        //}

        public async Task<List<ReviewCard_VM>> GetRecentReviewsAsync(int take)
        {
            var result = await FilterAllAsync(
                select: r => new ReviewCard_VM
                {
                    UserName = r.User.UserName,
                    BookID = r.Book.BookID,
                    CoverImage = r.Book.CoverImage,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt,
                    BookTitle = r.Book.Title
                },
                where: r => true,
                orderBy: r => r.OrderByDescending(r => r.CreatedAt), // Son yapılan yorumlar
                include: r => r.Include(r => r.User).Include(r => r.Book), // İlişkili User ve Book'u dahil et
                take: take // İstenen sayıda getirmek için
            );

            return result.ToList();
        }
    }


}
