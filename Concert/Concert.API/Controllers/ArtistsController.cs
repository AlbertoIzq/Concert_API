using Concert.API.Data;
using Concert.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ConcertDbContext _concertDbContext;

        public ArtistsController(ConcertDbContext concertDbContext)
        {
            _concertDbContext = concertDbContext;
        }

        // Get all artists
        // GET: https://localhost:portnumber/api/artists
        [HttpGet]
        public IActionResult GetAll()
        {
            var artists = _concertDbContext.Artists.ToList();

            return Ok(artists);
        }
    }
}
