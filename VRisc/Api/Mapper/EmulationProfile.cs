namespace VRisc.Presentation.Mapper;

using AutoMapper;
using VRisc.Core.Entities;
using VRisc.Api.DTOs;

public class EmulationProfile : Profile
{
    public EmulationProfile()
    {
        CreateMap<BusStateDto, BusState>()
            .ForMember(bus => bus.Dram, dram => dram.MapFrom(dto => Convert.FromBase64String(dto.Dram)));

        CreateMap<CpuStateDto, CpuState>();

        CreateMap<EmulationStateDto, EmulationState>();
    }
}