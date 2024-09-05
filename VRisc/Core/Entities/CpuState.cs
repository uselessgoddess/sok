namespace VRisc.Core.Entities;

public class CpuState
{
    public uint Pc { get; set; } = 0;

    public uint[] Xregs { get; set; } = [];

    public float[] Fregs { get; set; } = [];

    public BusState Bus { get; set; } = new();
}