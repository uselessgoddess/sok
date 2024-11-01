namespace VRisc.Core.Entities;

public class EmulationState(string user, string name)
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = name;

    public string User { get; set; } = user;

    public DateTime? Creation { get; set; } = DateTime.Now;

    public DateTime? Modified { get; set; } = DateTime.Now;

    public CpuState Cpu { get; set; } = new();
}