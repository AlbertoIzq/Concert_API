using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.API.Repositories;
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
        private readonly IArtistRepository _artistRepository;

        public ArtistsController(ConcertDbContext concertDbContext, IArtistRepository artistRepository)
        {
            _concertDbContext = concertDbContext;
            _artistRepository = artistRepository;
        }

        // GET ALL ARTISTS
        // GET: https://localhost:portnumber/api/artists
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Domain Model
            var artistsDomainModel = await _artistRepository.GetAllAsync();

            // Map Domain Model to DTO
            var artistsDto = new List<ArtistDto>();
            foreach (var artistDomainModel in artistsDomainModel)
            {
                artistsDto.Add(new ArtistDto()
                {
                    Id = artistDomainModel.Id,
                    Name = artistDomainModel.Name,
                    ArtistImageUrl = artistDomainModel.ArtistImageUrl
                });
            }

            // Return DTO back to client
            return Ok(artistsDto);
        }

        // GET SINGLE ARTIST (Get Artist by Id)
        // GET: https://localhost:portnumber/api/artists/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get data from database - Domain Model
            var artistDomainModel = await _concertDbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (artistDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var artistDto = new ArtistDto()
            {
                Id = artistDomainModel.Id,
                Name = artistDomainModel.Name,
                ArtistImageUrl = artistDomainModel.ArtistImageUrl
            };

            // Return DTO back to client
            return Ok(artistDto);
        }

        // CREATE NEW ARTIST
        // POST: https://localhost:portnumber/api/artists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddArtistRequestDto addArtistRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var artistDomainModel = new Artist()
            {
                Name = addArtistRequestDto.Name,
                ArtistImageUrl = addArtistRequestDto.ArtistImageUrl
            };

            // Use Domain Model to create Artist
            await _concertDbContext.Artists.AddAsync(artistDomainModel);
            await _concertDbContext.SaveChangesAsync();

            // Map Domain Model back to DTO
            var artistDto = new ArtistDto()
            {
                Id = artistDomainModel.Id,
                Name = artistDomainModel.Name,
                ArtistImageUrl = artistDomainModel.ArtistImageUrl
            };

            // Show information to the client
            return CreatedAtAction(nameof(GetById), new { id = artistDomainModel.Id }, artistDto);
        }

        // UPDATE ARTIST
        // PUT: https://localhost:portnumber/api/artists{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArtistRequestDto updateArtistRequestDto)
        {
            // Check if artist exists
            var artistDomainModel = await _concertDbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (artistDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            artistDomainModel.Name = updateArtistRequestDto.Name;
            artistDomainModel.ArtistImageUrl = updateArtistRequestDto.ArtistImageUrl;

            await _concertDbContext.SaveChangesAsync();

            // Convert Domain Model to DTO
            var artistDto = new ArtistDto()
            {
                Id = artistDomainModel.Id,
                Name = artistDomainModel.Name,
                ArtistImageUrl = artistDomainModel.ArtistImageUrl
            };

            return Ok(artistDto);
        }

        // DELETE ARTIST
        // DELETE: https://localhost:portnumber/api/artists{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Get data from database - Domain Model
            var artistDomainModel = await _concertDbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (artistDomainModel == null)
            {
                return NotFound();
            }

            // Delete region
            _concertDbContext.Artists.Remove(artistDomainModel);
            await _concertDbContext.SaveChangesAsync();

            // Return deleted artist back to the client
            // Convert Domain Model to DTO
            var artistDto = new ArtistDto()
            {
                Id = artistDomainModel.Id,
                Name = artistDomainModel.Name,
                ArtistImageUrl = artistDomainModel.ArtistImageUrl
            };

            // Return DTO back to client
            return Ok(artistDto);
        }
    }
}
