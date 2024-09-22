using Concert.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.API.Data
{
    public class ConcertDbContext : DbContext
    {
        public ConcertDbContext(DbContextOptions<ConcertDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Data to seed
            var genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = Guid.Parse("9fc5f185-c6c3-4bcd-90c0-74e35304d69c"),
                    Name = "Reagge"
                },
                new Genre()
                {
                    Id = Guid.Parse("5fb21249-08bc-4585-ae71-48392889955f"),
                    Name = "EBM"
                },
                new Genre()
                {
                    Id = Guid.Parse("f3a2308a-3774-4882-8cab-e1b52ce0b48a"),
                    Name = "EDM"
                }
            };

            var languages = new List<Language>()
            {
                new Language()
                {
                    Id = Guid.Parse("a783935f-bace-44d2-88c6-bfac4f3f331e"),
                    Name = "English"
                },
                new Language()
                {
                    Id = Guid.Parse("42dc4be9-24e1-450d-a7f2-c0ae9d722880"),
                    Name = "French"
                },
            };

            // Seed to the database
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Language>().HasData(languages);
        }
    }
}