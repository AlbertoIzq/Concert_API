using Concert.API.Data;
using Concert.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Concert.API.Repositories
{
    public class SqlArtistRepository : IArtistRepository
    {
        private readonly ConcertDbContext _concertDbContext;

        public SqlArtistRepository(ConcertDbContext concertDbContext)
        {
            _concertDbContext = concertDbContext;
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _concertDbContext.Artists.ToListAsync();
        }
    }
}
