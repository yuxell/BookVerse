using BookWebApp.Manager.Abstract;
using BookWebApp.Models;
using BookWebApp.Repositories.Abstract;

namespace BookWebApp.Manager.Concrete
{
    public class PublisherManager : IPublisherManager
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherManager(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<List<Publisher>> GetAllPublisherAsync()
        {
            return await _publisherRepository.GetAllAsync();
        }
    }
}
