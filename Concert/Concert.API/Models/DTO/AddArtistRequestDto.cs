using System.ComponentModel.DataAnnotations;

namespace Concert.API.Models.DTO
{
    public class AddArtistRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }
        public string? ArtistImageUrl { get; set; }
    }
}
