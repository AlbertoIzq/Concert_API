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

        public async Task<List<Song>> GetAllAsync()
        {
            return await _concertDbContext.Songs
                .Include(x => x.Artist)
                .Include("Genre")
                .Include("Language")
                .ToListAsync();
        }

        public async Task<Song?> GetByIdAsync(Guid id)
        {
            return await _concertDbContext.Songs
                .Include("Artist")
                .Include("Genre")
                .Include("Language")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Song?> UpdateAsync(Guid id, Song song)
        {
            // Check if it exists
            var existingSong = await _concertDbContext.Songs.FirstOrDefaultAsync(x => x.Id == id);

            if (existingSong == null)
            {
                return null;
            }

            // Assign updated values
            existingSong.Title = song.Title;
            existingSong.Album = song.Album;
            existingSong.Length = song.Length;
            existingSong.ReleaseYear = song.ReleaseYear;
            existingSong.SongImageUrl = song.SongImageUrl;
            existingSong.ArtistId = song.ArtistId;
            existingSong.LanguageId = song.LanguageId;
            existingSong.GenreId = song.GenreId;

            await _concertDbContext.SaveChangesAsync();

            return existingSong;
        }

        public async Task<Song?> DeleteAsync(Guid id)
        {
            // Check if it exists
            var existingSong= await _concertDbContext.Songs.FirstOrDefaultAsync(x => x.Id == id);

            if (existingSong == null)
            {
                return null;
            }

            // Delete region
            _concertDbContext.Songs.Remove(existingSong);
            await _concertDbContext.SaveChangesAsync();

            return existingSong;
        }
    }
}
