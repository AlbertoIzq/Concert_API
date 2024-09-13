using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
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
            // Get data from database - Domain Model
            var artistsDomain = _concertDbContext.Artists.ToList();

            // Map Domain Model to DTO
            var artistsDto = new List<ArtistDto>();
            foreach (var artistDomain in artistsDomain)
            {
                artistsDto.Add(new ArtistDto()
                {
                    Id = artistDomain.Id,
                    Name = artistDomain.Name,
                    ArtistImageUrl = artistDomain.ArtistImageUrl
                });
            }

            // Return DTO back to client
            return Ok(artistsDto);
        }

        // GET SINGLE ARTIST (Get Artist by Id)
        // GET: https://localhost:portnumber/api/artists/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Get data from database - Domain Model
            var artistDomain = _concertDbContext.Artists.FirstOrDefault(x => x.Id == id);

            if (artistDomain == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var artistDto = new ArtistDto()
            {
                Id = artistDomain.Id,
                Name = artistDomain.Name,
                ArtistImageUrl = artistDomain.ArtistImageUrl
            };

            // Return DTO back to client
            return Ok(artistDto);
        }

        // CREATE NEW ARTIST
        // POST: https://localhost:portnumber/api/artists
        [HttpPost]
        public IActionResult Create([FromBody] AddArtistRequestDto addArtistRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var artistDomain = new Artist()
            {
                Name = addArtistRequestDto.Name,
                ArtistImageUrl = addArtistRequestDto.ArtistImageUrl
            };

            // Use Domain Model to create Artist
            _concertDbContext.Artists.Add(artistDomain);
            _concertDbContext.SaveChanges();

            // Map Domain Model back to DTO
            var artistDto = new ArtistDto()
            {
                Id = artistDomain.Id,
                Name = artistDomain.Name,
                ArtistImageUrl = artistDomain.ArtistImageUrl
            };

            // Show information to the client
            return CreatedAtAction(nameof(GetById), new { id = artistDomain.Id }, artistDto);
        }
    }
}
