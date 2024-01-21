using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<NewRegionDto, Region>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<NewWalkDto, Walk>().ReverseMap();
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        CreateMap<UpdateWalkDto, Walk>().ReverseMap();
    }
}