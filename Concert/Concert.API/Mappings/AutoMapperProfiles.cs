using AutoMapper;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;

namespace Concert.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // From Model to Model
            CreateMap<AddArtistRequestDto, Artist>();
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<UpdateArtistRequestDto, Artist>();

            CreateMap<AddSongRequestDto, Song>();
            CreateMap<Song, SongDto>().ReverseMap();
            CreateMap<UpdateSongRequestDto, Song>();

            CreateMap<Language, LanguageDto>().ReverseMap();

            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
