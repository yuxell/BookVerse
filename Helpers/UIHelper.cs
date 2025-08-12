using BookWebApp.Models.ViewModels;
using BookWebApp.Models;
using BookWebApp.Models.ViewModels.BookVM;

namespace BookWebApp.Helpers
{
    public static class UIHelper
    {
        public static Book ConvertToBook(BookEdit_VM vm, Book existingBook = null)
        {
            if (existingBook == null)
            {
                existingBook = new Book();
            }

            existingBook.BookID = vm.BookID;
            existingBook.Title = vm.Title;
            existingBook.Price = vm.Price;
            existingBook.Description = vm.Description;
            existingBook.CoverImage = vm.CoverImage;
            existingBook.AuthorID = vm.AuthorID;
            existingBook.CategoryID = vm.CategoryID;
            existingBook.AverageRating = vm.AverageRating;

            // Publisher ilişkisini güncelleme
            existingBook.BookPublishers = vm.BookPublishers.Select(bp => new BookPublisher
            {
                BookID = vm.BookID,
                PublisherID = bp.PublisherID
            }).ToList();

            return existingBook;
        }
    }
}
