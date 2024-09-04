using VRisc.Core.Entities;

namespace VRisc.Core.Interfaces;

public interface IEmulator
{
    Trap NextCycle(ref CpuState state);

    CpuState Run(Action<CpuState> sync, TimeSpan span, CancellationToken token = default)
    {
        return default;
    }
}