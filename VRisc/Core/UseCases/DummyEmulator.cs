using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

namespace VRisc.Core.UseCases;

public class DummyEmulator : IEmulator
{
    public Trap NextCycle(ref CpuState state)
    {
        throw new NotImplementedException();
    }
}