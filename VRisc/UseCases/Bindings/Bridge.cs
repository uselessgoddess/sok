namespace VRisc.UseCases.Bindings;

using VRisc.Core.Bindings;
using VRisc.Core.Entities;

public static class Bridge
{
    public static CpuState FromRepr(CpuRepr cpu)
    {
        unsafe
        {
            var xregs = new ulong[32];

            for (var i = 0; i < 32 && i < cpu.xregs.len; i++)
            {
                xregs[i] = cpu.xregs.regs[i];
            }

            return new CpuState
            {
                Pc = cpu.pc,
                Mode = cpu.mode,
                Xregs = xregs,
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
                mode = cpu.Mode,
                bus = new BusRepr
                {
                    dram = Slice<byte>.FromArray(cpu.Bus.Dram),
                },
            };

            for (var i = 0; i < 32 && i < cpu.Xregs.Length; i++)
            {
                repr.xregs.regs[i] = cpu.Xregs[i];
            }

            repr.xregs.len = (byte)cpu.Xregs.Length;

            return repr;
        }
    }
}