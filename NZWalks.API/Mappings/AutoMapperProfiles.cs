using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Request;
using NZWalks.API.Models.DTO.Response;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            //for region
            CreateMap<Region, RegionResponseDto>();
            CreateMap<AddRegionRequestDto, Region>();
            CreateMap<UpdateRegionRequestDto, Region>();

            //for walk
            CreateMap<AddWalkRequestDto, Walk>();
            CreateMap<Walk, WalkResponseDto>();
            CreateMap<Difficulty, DifficultyResponseDto>();
        }
    }
}
