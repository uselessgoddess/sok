namespace VRisc.Core.UseCases;

using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class DummyEmulator : IEmulator
{
    public Trap NextCycle()
    {
        return Trap.Requested;
    }

    public CpuState GetState()
    {
        return new CpuState();
    }
}