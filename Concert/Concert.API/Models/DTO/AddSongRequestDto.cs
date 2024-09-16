namespace Concert.API.Models.DTO
{
    public class AddSongRequestDto
    {
        public string Title { get; set; }
        public string? Album { get; set; }
        public TimeSpan Length { get; set; }
        public int ReleaseYear { get; set; }
        public string? SongImageUrl { get; set; }
        public Guid ArtistId { get; set; }
        public Guid LanguageId { get; set; }
        public Guid GenreId { get; set; }
    }
}
