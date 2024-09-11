namespace Concert.API.Models.Domain
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Album { get; set; }
        public TimeSpan Length { get; set; }
        public int ReleaseYear { get; set; }
        public string? SongImageUrl { get; set; }
        public Guid ArtistId { get; set; }
        public Guid LanguageId { get; set; }
        public Guid GenreId { get; set; }

        // Navigation properties
        public Artist Artist { get; set;}
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}
