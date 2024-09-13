using Concert.API.Models.Domain;

namespace Concert.API.Repositories
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
    }
}
