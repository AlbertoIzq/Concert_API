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

            // AddArtistRequestDto to Artist
            CreateMap<AddArtistRequestDto, Artist>();

            // UpdateArtistRequestDto to Artist
            CreateMap<UpdateArtistRequestDto, Artist>();
        }
    }
}
