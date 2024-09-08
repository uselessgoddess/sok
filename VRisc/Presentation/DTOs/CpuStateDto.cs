namespace VRisc.Presentation.DTOs;

public class CpuStateDto
{
    public ulong Pc { get; set; } = 0;

    public ulong[] Xregs { get; set; } = [];

    public float[] Fregs { get; set; } = [];

    public BusStateDto Bus { get; set; } = new();
}