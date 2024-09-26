using AutoMapper;
using Concert.API.CustomActionFilters;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.API.Repositories;
using Concert.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Concert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ArtistsController> _logger;

        public ArtistsController(IArtistRepository artistRepository,
            IMapper mapper,
            ILogger<ArtistsController> logger)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // CREATE Artist
        // POST: https://localhost:portnumber/api/artists
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = SD.WRITER_ROLE_NAME)]
        public async Task<IActionResult> Create([FromBody] AddArtistRequestDto addArtistRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var artistDomainModel = _mapper.Map<Artist>(addArtistRequestDto);

            // Use Domain Model to create Artist
            await _artistRepository.CreateAsync(artistDomainModel);

            // Map Domain Model back to DTO
            var artistDto = _mapper.Map<ArtistDto>(artistDomainModel);

            // Show information to the client
            return CreatedAtAction(nameof(GetById), new { id = artistDomainModel.Id }, artistDto);
        }

        // GET ALL Artists
        // GET: https://localhost:portnumber/api/artists
        [HttpGet]
        //[Authorize(Roles = SD.READER_ROLE_NAME)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll Artists action method was invoked");

            // Get data from database - Domain Model
            var artistsDomainModel = await _artistRepository.GetAllAsync();

            // Map Domain Model to DTO
            var artistsDto = _mapper.Map<List<ArtistDto>>(artistsDomainModel);

            _logger.LogInformation($"Finished GetAll Artists request with data: {JsonSerializer.Serialize(artistsDto)}");

            // Return DTO back to client
            return Ok(artistsDto);
        }

        // GET SINGLE ARTIST (Get Artist by Id)
        // GET: https://localhost:portnumber/api/artists/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = SD.READER_ROLE_NAME)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get data from database - Domain Model
            var artistDomainModel = await _artistRepository.GetByIdAsync(id);

            if (artistDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var artistDto = _mapper.Map<ArtistDto>(artistDomainModel);

            // Return DTO back to client
            return Ok(artistDto);
        }

        // UPDATE ARTIST
        // PUT: https://localhost:portnumber/api/artists{id}
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = SD.WRITER_ROLE_NAME)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArtistRequestDto updateArtistRequestDto)
        {
            // Map DTO to Domain Model
            var artistDomainModel = _mapper.Map<Artist>(updateArtistRequestDto);

            // Update artist if it exists
            artistDomainModel = await _artistRepository.UpdateAsync(id, artistDomainModel);

            if (artistDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var artistDto = _mapper.Map<ArtistDto>(artistDomainModel);

            return Ok(artistDto);
        }

        // DELETE ARTIST
        // DELETE: https://localhost:portnumber/api/artists{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = SD.WRITER_ROLE_NAME)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete artist if it exists
            var artistDomainModel = await _artistRepository.DeleteAsync(id);

            if (artistDomainModel == null)
            {
                return NotFound();
            }

            // Return deleted artist back to the client
            // Convert Domain Model to DTO
            var artistDto = _mapper.Map<ArtistDto>(artistDomainModel);

            // Return DTO back to client
            return Ok(artistDto);
        }
    }
}