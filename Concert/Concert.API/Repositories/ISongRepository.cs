using Concert.API.Models.Domain;
using Concert.Utility;

namespace Concert.API.Repositories
{
    public interface ISongRepository
    {
        Task<Song> CreateAsync(Song song);
        Task<List<Song>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = SD.SONG_DEFAULT_PAGE_SIZE);
        Task<Song?> GetByIdAsync(Guid id);
        Task<Song?> UpdateAsync(Guid id, Song song);
        Task<Song?> DeleteAsync(Guid id);
    }
}
