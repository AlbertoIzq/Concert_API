namespace Concert.API.Models.DTO
{
    public class SongDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Album { get; set; }
        public TimeSpan Length { get; set; }
        public int ReleaseYear { get; set; }
        public string? SongImageUrl { get; set; }

        public ArtistDto Artist { get; set; }
        public LanguageDto Language { get; set; }
        public GenreDto Genre { get; set; }
    }
}
