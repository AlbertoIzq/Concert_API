﻿using AutoMapper;
using Concert.API.CustomActionFilters;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.API.Repositories;
using Concert.Utility;
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
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddSongRequestDto addSongRequestDto)
        {
            // Map DTO to Domain Model
            var songDomainModel = _mapper.Map<Song>(addSongRequestDto);

            // Create Song
            songDomainModel = await _songRepository.CreateAsync(songDomainModel);

            // Map Domain Model back to DTO
            var songDto = _mapper.Map<SongDto>(songDomainModel);

            // Show information to the client
            return Ok(songDto);
        }

        // GET ALL Songs
        // GET: api/songs?filterOn=PropertyName&filterQuery=PropertyValue&sortBy=PropertyName&isAscending=true&pageNumber=x&pageSize=y
        // e.g. filterOn=Title&filterQuery=military&sortBy=Title&isAscending=false&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = SD.SONG_DEFAULT_PAGE_SIZE)
        {
            // Get all songs
            var songsDomainModel = await _songRepository.GetAllAsync(filterOn, filterQuery,
                sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            var songsDto = _mapper.Map<List<SongDto>>(songsDomainModel);

            // Return DTO to the client
            return Ok(songsDto);
        }

        // GET Song by Id
        // GET: api/songs/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get data from database - Domain Model
            var songDomainModel = await _songRepository.GetByIdAsync(id);

            if (songDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var songDto = _mapper.Map<SongDto>(songDomainModel);

            // Return DTO back to client
            return Ok(songDto);
        }

        // UPDATE Song
        // PUT: api/songs/{id}
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateSongRequestDto updateSongRequestDto)
        {
            // Map DTO to Domain Model
            var songDomainModel = _mapper.Map<Song>(updateSongRequestDto);

            // Update if it exists
            songDomainModel = await _songRepository.UpdateAsync(id, songDomainModel);

            if (songDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var songDto = _mapper.Map<SongDto>(songDomainModel);

            // Return DTO back to the client
            return Ok(songDto);
        }

        // DELETE Song
        // DELETE: api/songs/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete if it exists
            var songDomainModel = await _songRepository.DeleteAsync(id);

            if (songDomainModel == null)
            {
                return NotFound();
            }

            // Return deleted back to the client
            // Convert Domain Model to DTO
            var songDto = _mapper.Map<SongDto>(songDomainModel);

            // Return DTO back to client
            return Ok(songDto);
        }
    }
}
