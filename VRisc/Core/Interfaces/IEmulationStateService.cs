using VRisc.Core.Entities;

namespace VRisc.Core.Interfaces;

public interface IEmulationStateService
{
    EmulationState GetState();

    void SetState(EmulationState state);
}