using BookWebApp.Business.Utilities;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels;
using BookWebApp.Models.ViewModels.BookVM;

namespace BookWebApp.Manager.Abstract
{
    public interface IBookManager
    {

        /// <summary>
        /// Kitabın adına veya yazarına göre arama yapar
        /// </summary>
        /// <param name="searchTerm">Kitap adı veya yazar adı veya soyadı</param>
        /// <returns></returns>
        Task<List<Book>> SearchBooksAsync(string searchTerm);
        Task<List<Book>> GetBooksByTitleAsync(string title);
        Task<List<Book>> GetBooksByCategoryAsync(int categoryId);
        Task<List<Book>> GetBooksByAuthorAsync(int author);
        Task<List<BookCard_VM>> GetBookCardsAsync();
        Task<List<BookCard_VM>> GetBookCardsFuturedAsync(int takeCount);

        Task<Result>  AddBookAsync(Book book);

        // Litaplar listelenirken 'load more' butonu eklemek için
        Task<List<BookCard_VM>> LoadMoreBooks(int page = 1, int pageSize = 10);
        Task<BookDetail_VM> GetBooksByIDAsync(int bookID);
        Task<Book> GetBooksByIDForUpdateAsync(int bookID);

        Task<Result> UpdateBookAsync(Book book);
        Task UpdateAverageRatingAsync(int bookId, double newAverageRating);

        Task<int> GetBookCountAsync();





    }
}
