namespace VRisc.Core.UseCases;

using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class DummyEmulator : IEmulator
{
    public Trap NextCycle(ref CpuState state)
    {
        return Trap.Requested;
    }
}