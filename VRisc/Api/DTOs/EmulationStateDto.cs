namespace VRisc.Api.DTOs;

public class EmulationStateDto
{
    public string? Id { get; set; }

    public string User { get; set; }

    public string Name { get; set; }

    public DateTime Creation { get; set; }

    public DateTime Modified { get; set; }

    public CpuStateDto Cpu { get; set; }
}
