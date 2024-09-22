using Concert.API.Models.Domain;
using Concert.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.API.Data
{
    public class ConcertAuthDbContext : IdentityDbContext
    {
        public ConcertAuthDbContext(DbContextOptions<ConcertAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = SD.READER_ROLE_ID,
                    ConcurrencyStamp = SD.READER_ROLE_ID,
                    Name = SD.READER_ROLE_NAME,
                    NormalizedName = SD.READER_ROLE_NAME.ToUpper()
                },
                new IdentityRole
                {
                    Id = SD.WRITER_ROLE_ID,
                    ConcurrencyStamp = SD.WRITER_ROLE_ID,
                    Name = SD.WRITER_ROLE_NAME,
                    NormalizedName = SD.WRITER_ROLE_NAME.ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}