namespace VRisc.Core.Entities;

public class EmulationState
{
    public string? Id { get; set; }
    public string User { get; set; }
    public DateTime Creation { get; set; }
    public DateTime Modified { get; set; }

    public CpuState Cpu { get; set; }
}