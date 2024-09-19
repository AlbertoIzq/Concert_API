using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<List<Song>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = SD.SONG_DEFAULT_PAGE_SIZE)
        {
            var songs = _concertDbContext.Songs
                .Include(x => x.Artist)
                .Include("Genre")
                .Include("Language").AsQueryable();

            // Filtering
            if (!filterOn.IsNullOrEmpty() && !filterQuery.IsNullOrEmpty())
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    songs = songs.Where(x => x.Title.Contains(filterQuery));
                }
            }

            // Sorting
            if (!sortBy.IsNullOrEmpty())
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    songs = isAscending ? songs.OrderBy(x => x.Title) : songs.OrderByDescending(x => x.Title);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await songs.Skip(skipResults).Take(pageSize).ToListAsync();
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
