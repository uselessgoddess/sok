namespace VRisc.Core.Entities;

public struct EmulationResult
{
    public CpuState Cpu { get; set; }

    public bool Fatal { get; set; }
}