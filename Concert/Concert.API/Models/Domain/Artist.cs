namespace Concert.API.Models.Domain
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ArtistImageUrl { get; set; }
    }
}
