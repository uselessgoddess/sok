namespace VRisc.Core.Entities;

public class CpuState
{
    public ulong Pc { get; set; } = 0;

    public Mode Mode { get; set; } = Mode.User;

    public ulong[] Xregs { get; set; } = [];

    public float[] Fregs { get; set; } = [];

    public BusState Bus { get; set; } = new();
}