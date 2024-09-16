using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Concert.API.Repositories
{
    public class SqlSongRepository : ISongRepository
    {
        private readonly ConcertDbContext _concertDbContext;

        public SqlSongRepository(ConcertDbContext concertDbContext)
        {
            _concertDbContext = concertDbContext;
        }

        public async Task<Song> CreateAsync(Song song)
        {
            await _concertDbContext.Songs.AddAsync(song);
            await _concertDbContext.SaveChangesAsync();
            return song;
        }
    }
}
