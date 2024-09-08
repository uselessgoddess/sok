namespace VRisc.Core.Bindings;

using VRisc.Core.Entities;

public static class XSlice
{
    public static unsafe Slice<ulong> FromXregs(ulong* xregs)
    {
        return new Slice<ulong> { ptr = xregs, len = 32 };
    }

    public static unsafe ulong* IntoXregs(this Slice<ulong> slice)
    {
        return slice.ptr;
    }
}

public static class Bridge
{
    public static CpuState FromRepr(CpuRepr cpu)
    {
        unsafe
        {
            return new CpuState
            {
                Pc = cpu.pc,
                Xregs = XSlice.FromXregs(cpu.xregs).IntoArray(),
                Bus = new BusState
                {
                    Dram = cpu.bus.dram.IntoArray(),
                },
            };
        }
    }

    public static CpuRepr IntoRepr(this CpuState cpu)
    {
        unsafe
        {
            var repr = new CpuRepr
            {
                pc = cpu.Pc,
                bus = new BusRepr
                {
                    dram = Slice<byte>.FromArray(cpu.Bus.Dram),
                },
            };

            for (var i = 0; i < 32; i++)
            {
                repr.xregs[i] = cpu.Xregs[i];
            }

            return repr;
        }
    }
}