namespace VRisc.Api.DTOs;

public class EmulationInputDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public CpuStateDto Cpu { get; set; }
}
