using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
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

        public async Task<Artist> CreateAsync(Artist artist)
        {
            await _concertDbContext.Artists.AddAsync(artist);
            await _concertDbContext.SaveChangesAsync();
            return artist;
        }

        public async Task<Artist?> DeleteAsync(Guid id)
        {
            // Check if artist exists
            var existingArtist = await _concertDbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (existingArtist == null)
            {
                return null;
            }

            // Delete region
            _concertDbContext.Artists.Remove(existingArtist);
            await _concertDbContext.SaveChangesAsync();

            return existingArtist;
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _concertDbContext.Artists.ToListAsync();
        }

        public async Task<Artist?> GetByIdAsync(Guid id)
        {
            return await _concertDbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Artist?> UpdateAsync(Guid id, Artist artist)
        {
            // Check if artist exists
            var existingArtist = await _concertDbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (existingArtist == null)
            {
                return null;
            }

            // Assign updated values
            existingArtist.Name = artist.Name;
            existingArtist.ArtistImageUrl = artist.ArtistImageUrl;

            await _concertDbContext.SaveChangesAsync();

            return existingArtist;
        }
    }
}
