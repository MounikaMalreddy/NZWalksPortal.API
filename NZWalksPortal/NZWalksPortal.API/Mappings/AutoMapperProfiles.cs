using AutoMapper;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Models.DTO;

namespace NZWalksPortal.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();

        }
    }
}
