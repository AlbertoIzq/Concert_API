using Concert.API.Models.Domain;

namespace Concert.API.Repositories
{
    public interface ISongRepository
    {
        Task<Song> CreateAsync(Song song);
        Task<List<Song>> GetAllAsync();
        Task<Song?> GetByIdAsync(Guid id);
    }
}
