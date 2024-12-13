namespace VRisc.Presentation.Mapper;

using AutoMapper;
using VRisc.Core.Entities;
using VRisc.Api.DTOs;

public class EmulationProfile : Profile
{
    public EmulationProfile()
    {
        CreateMap<BusState, BusStateDto>()
            .ForMember(bus => bus.Dram, dram => dram.MapFrom(dto => Convert.ToBase64String(dto.Dram)));
        CreateMap<BusStateDto, BusState>()
            .ForMember(bus => bus.Dram, dram => dram.MapFrom(dto => Convert.FromBase64String(dto.Dram)));
        CreateMap<CpuState, CpuStateDto>();
        CreateMap<CpuStateDto, CpuState>();

        CreateMap<EmulationState, EmulationInfoDto>();
        CreateMap<EmulationState, EmulationStateDto>();
        CreateMap<EmulationStateDto, EmulationState>();
    }
}