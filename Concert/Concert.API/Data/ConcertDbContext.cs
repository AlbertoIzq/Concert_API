using Concert.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.API.Data
{
    public class ConcertDbContext : DbContext
    {
        public ConcertDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}