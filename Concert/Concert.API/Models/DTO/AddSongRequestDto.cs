using System.ComponentModel.DataAnnotations;

namespace Concert.API.Models.DTO
{
    public class AddSongRequestDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Title has to be a maximum of 200 characters")]
        public string Title { get; set; }
        public string? Album { get; set; }
        [Required]
        public TimeSpan Length { get; set; }
        [Required]
        [Range(500, 2100, ErrorMessage = "Release year has to be between 500 and 2100")]
        public int ReleaseYear { get; set; }
        public string? SongImageUrl { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        [Required]
        public Guid LanguageId { get; set; }
        [Required]
        public Guid GenreId { get; set; }
    }
}
