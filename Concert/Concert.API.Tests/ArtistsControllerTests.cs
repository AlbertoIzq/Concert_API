using AutoMapper;
using Concert.API.Controllers;
using Concert.API.Mappings;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Concert.API.Tests
{
    public class ArtistsControllerTests
    {
        private readonly Mock<IArtistRepository> _artistRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ArtistsController>> _loggerMock;

        private readonly ArtistsController _sut;

        public ArtistsControllerTests()
        {
            _artistRepositoryMock = new Mock<IArtistRepository>();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mapper.CreateMapper();

            _loggerMock = new Mock<ILogger<ArtistsController>>();

            _sut = new ArtistsController(_artistRepositoryMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsOk()
        {
            // Arrange
            _artistRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Artist>())).ReturnsAsync(_ArtistCreate);

            // Act
            var actionResult = await _sut.Create(_addArtistRequestDtoCreate);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var result = Assert.IsAssignableFrom<ArtistDto>(createdResult.Value);
            var expected = _ArtistDtoCreate;
            Assert.Equal(expected, result);
        }

        private AddArtistRequestDto _addArtistRequestDtoCreate = new AddArtistRequestDto
        {
            Name = "Artist test",
            ArtistImageUrl = null
        };

        private Artist _ArtistCreate = new Artist
        {
            Id = Guid.Parse("9fc5f185-c6c3-4bcd-90c0-74e35304d69c"),
            Name = "Artist test",
            ArtistImageUrl = null
        };

        private ArtistDto _ArtistDtoCreate = new ArtistDto
        {
            Id = Guid.Parse("9fc5f185-c6c3-4bcd-90c0-74e35304d69c"),
            Name = "Artist test",
            ArtistImageUrl = null
        };
    }
}