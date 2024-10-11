namespace Concert.API.Models.DTO
{
    public class ArtistDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ArtistImageUrl { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is ArtistDto)
            {
                var that = obj as ArtistDto;
                return that.Id == this.Id &&
                    that.Name == this.Name &&
                    that.ArtistImageUrl == this.ArtistImageUrl;
            }

            return false;
        }
    }
}
