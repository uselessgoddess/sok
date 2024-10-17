namespace VRisc.UseCases.Interfaces;

using VRisc.Core.Entities;
using VRisc.Core.Exceptions;

public interface IEmulationStatesService
{
    EmulationState? GetState(string user);

    void SetState(string user, EmulationState state);

    void UpdateState(string user, Func<EmulationState, EmulationState> update)
    {
        var state = GetState(user) ?? throw new NotFoundException();

        SetState(user, update(state));
    }
}