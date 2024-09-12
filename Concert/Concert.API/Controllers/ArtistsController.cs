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

        // GET ALL ARTISTS
        // GET: https://localhost:portnumber/api/artists
        [HttpGet]
        public IActionResult GetAll()
        {
            var artists = _concertDbContext.Artists.ToList();

            return Ok(artists);
        }

        // GET SINGLE ARTIST (Get Artist by Id)
        // GET: https://localhost:portnumber/api/artists/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Find() only takes primary key as a parameter
            //var artist = _concertDbContext.Artists.Find(id);

            var artist = _concertDbContext.Artists.FirstOrDefault(x => x.Id == id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }
    }
}
