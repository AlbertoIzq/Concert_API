namespace Concert.API.Models.Domain
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double PriceFixed { get; set; }
        public double PricePerSong { get; set; }
    }
}
