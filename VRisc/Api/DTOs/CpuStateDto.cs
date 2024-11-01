namespace VRisc.Api.DTOs;

public class CpuStateDto
{
    public ulong Pc { get; set; }

    public ulong[] Xregs { get; set; }

    public float[] Fregs { get; set; }

    public BusStateDto Bus { get; set; }
}