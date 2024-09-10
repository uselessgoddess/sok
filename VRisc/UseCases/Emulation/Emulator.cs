using VRisc.UseCases.Bindings;

namespace VRisc.UseCases.Emulation;

using VRisc.Core.Bindings;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class Emulator : IEmulator
{
    public const ulong RAM = 1024 * 1024;

    public readonly unsafe void* _inner;

    public Emulator(CpuState cpu)
    {
        unsafe
        {
            _inner = NativeEmulator.vempty_emu(RAM);
            NativeEmulator.vmap_emu(_inner, new EmuRepr { cpu = cpu.IntoRepr() });
        }
    }

    ~Emulator()
    {
        unsafe
        {
            NativeEmulator.vfree_emu(_inner);
        }
    }

    public Trap NextCycle()
    {
        unsafe
        {
            return NativeEmulator.vcycle_emu(_inner);
        }
    }

    public CpuState GetState()
    {
        unsafe
        {
            return Bridge.FromRepr(NativeEmulator.vcpu_repr(_inner));
        }
    }
}