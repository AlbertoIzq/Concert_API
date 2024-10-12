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
            _artistRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Artist>())).ReturnsAsync(_ArtistCreateAndGetById);

            // Act
            var actionResult = await _sut.Create(_addArtistRequestDtoCreate);

            // Assert
            var result = Assert.IsType<CreatedAtActionResult>(actionResult);
            var resultValue = Assert.IsAssignableFrom<ArtistDto>(result.Value);
            var expected = _ArtistDtoCreateAndGetById;
            Assert.Equal(expected, resultValue);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            // Arrange
            _artistRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_ArtistsGetAll);

            // Act
            var actionResult = await _sut.GetAll();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            var resultValue = Assert.IsAssignableFrom<List<ArtistDto>>(result.Value);
            var expected = _ArtistsDtoGetAll;
            Assert.Equal(expected, resultValue);
        }

        [Fact]
        public async Task GetById_ReturnsOk()
        {
            // Arrange
            _artistRepositoryMock.Setup(x => x.GetByIdAsync(_guidGetById)).ReturnsAsync(_ArtistCreateAndGetById);

            // Act
            var actionResult = await _sut.GetById(_guidGetById);

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            var resultValue = Assert.IsAssignableFrom<ArtistDto>(result.Value);
            var expected = _ArtistDtoCreateAndGetById;
            Assert.Equal(expected, resultValue);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            // Arrange
            _artistRepositoryMock.Setup(x => x.GetByIdAsync(_guidGetById)).ReturnsAsync(_ArtistCreateAndGetById);

            // Act
            var actionResult = await _sut.GetById(_guidGetByIdNotFound);

            // Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
        }

        #region TestData

        private AddArtistRequestDto _addArtistRequestDtoCreate = new AddArtistRequestDto
        {
            Name = "Artist test",
            ArtistImageUrl = null
        };

        private Artist _ArtistCreateAndGetById = new Artist
        {
            Id = Guid.Parse("9fc5f185-c6c3-4bcd-90c0-74e35304d69c"),
            Name = "Artist test",
            ArtistImageUrl = null
        };

        private ArtistDto _ArtistDtoCreateAndGetById = new ArtistDto
        {
            Id = Guid.Parse("9fc5f185-c6c3-4bcd-90c0-74e35304d69c"),
            Name = "Artist test",
            ArtistImageUrl = null
        };

        private List<Artist> _ArtistsGetAll = new List<Artist>
        {
            new Artist
            {
                Id = Guid.Parse("BD49D4C3-849D-41A8-5925-08DCD6993C5C"),
                Name = "Ace of base"
            },
            new Artist
            {
                Id = Guid.Parse("F03ABFB0-397A-4790-D7F6-08DCD7FC4FB7"),
                Name = "Cypis"
            }
        };

        private List<ArtistDto> _ArtistsDtoGetAll = new List<ArtistDto>
        {
            new ArtistDto
            {
                Id = Guid.Parse("BD49D4C3-849D-41A8-5925-08DCD6993C5C"),
                Name = "Ace of base"
            },
            new ArtistDto
            {
                Id = Guid.Parse("F03ABFB0-397A-4790-D7F6-08DCD7FC4FB7"),
                Name = "Cypis"
            }
        };

        private Guid _guidGetById = Guid.Parse("9fc5f185-c6c3-4bcd-90c0-74e35304d69c");
        private Guid _guidGetByIdNotFound = Guid.Parse("9fc5f185-c6c3-4bcd-90c0-74e35304d69d");

        #endregion
    }
}