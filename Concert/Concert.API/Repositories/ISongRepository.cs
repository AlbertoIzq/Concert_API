using Concert.API.Models.Domain;

namespace Concert.API.Repositories
{
    public interface ISongRepository
    {
        Task<Song> CreateAsync(Song song);
        Task<List<Song>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true);
        Task<Song?> GetByIdAsync(Guid id);
        Task<Song?> UpdateAsync(Guid id, Song song);
        Task<Song?> DeleteAsync(Guid id);
    }
}
