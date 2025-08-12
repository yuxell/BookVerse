using BookWebApp.Models;

namespace BookWebApp.Manager.Abstract
{
    public interface IPublisherManager
    {
        Task<List<Publisher>> GetAllPublisherAsync();
    }
}
