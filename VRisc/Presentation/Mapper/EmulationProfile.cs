namespace VRisc.Presentation.Mapper;

using AutoMapper;
using VRisc.Core.Entities;
using VRisc.Presentation.DTOs;

public class EmulationProfile : Profile
{
    public EmulationProfile()
    {
        CreateMap<EmulationState, EmulationStateDto>();
        CreateMap<EmulationStateDto, EmulationState>();
    }
}