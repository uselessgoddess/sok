using VRisc.Core.Entities;
using VRisc.UseCases.Emulation;

namespace VRisc.Tests;

public class EmulatorTests
{
    [Fact]
    public void EmuRegisters()
    {
        var emulator = new Emulator(new CpuState
        {
            Pc = 0x80000000,
            Bus = new BusState { Dram = [0x93, 0x00, 0xb0, 0x07] },
        });

        Assert.Equal(Trap.Requested, emulator.NextCycle());

        Assert.Equal(123U, emulator.GetState().Xregs[1]);
    }
}