using AutoMapper;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concert.API.Controllers
{
    // api/songs
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISongRepository _songRepository;

        public SongsController(IMapper mapper,
            ISongRepository songRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
        }

        // CREATE Song
        // POST: api/songs
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSongRequestDto addSongRequestDto)
        {
            // Map DTO to Domain Model
            var songDomainModel = _mapper.Map<Song>(addSongRequestDto);

            // Create Song
            await _songRepository.CreateAsync(songDomainModel);

            // Map Domain Model back to DTO
            var songDto = _mapper.Map<SongDto>(songDomainModel);

            // Show information to the client
            return Ok(songDto);
        }

        // GET ALL Songs
        // GET: api/songs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get all songs
            var songsDomainModel = await _songRepository.GetAllAsync();

            // Map Domain Model to DTO
            var songsDto = _mapper.Map<List<SongDto>>(songsDomainModel);

            // Return DTO to the client
            return Ok(songsDto);
        }
    }
}
