namespace VRisc.Core.Entities;

public class CpuState
{
    public byte[] Src { get; set; }
    public uint Pc { get; set; }
    public uint[] Xregs { get; set; }
    public float[] Fregs { get; set; }
}