using Concert.API.Models.Domain;

namespace Concert.API.Repositories
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(Guid id);
        Task<Artist> CreateAsync(Artist artist);
        Task<Artist?> UpdateAsync(Guid id, Artist artist);
        Task<Artist?> DeleteAsync(Guid id);
    }
}
