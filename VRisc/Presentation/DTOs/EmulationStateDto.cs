namespace VRisc.Presentation.DTOs;

using VRisc.Core.Entities;

public class EmulationStateDto
{
    public DateTime Creation { get; set; }

    public DateTime Modified { get; set; }

    public CpuState Cpu { get; set; }
}