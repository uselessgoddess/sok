using VRisc.UseCases.Interfaces;

namespace VRisc.Infrastructure.Services;

using System.Collections.Concurrent;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class EmulationStatesService : IEmulationStatesService
{
    private readonly ConcurrentDictionary<string, EmulationState> states = new();

    public EmulationState? GetState(string user)
    {
        return states.GetValueOrDefault(user);
    }

    public void SetState(string user, EmulationState state)
    {
        states.AddOrUpdate(user, state, (_, _) => state);
    }
}