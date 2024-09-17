using AutoMapper;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;

namespace Concert.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Artist (Domain Model) to ArtistDto
            CreateMap<Artist, ArtistDto>().ReverseMap();

            CreateMap<AddArtistRequestDto, Artist>();
            CreateMap<UpdateArtistRequestDto, Artist>();

            CreateMap<AddSongRequestDto, Song>();
            CreateMap<Song, SongDto>().ReverseMap();

            CreateMap<Language, LanguageDto>().ReverseMap();

            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
