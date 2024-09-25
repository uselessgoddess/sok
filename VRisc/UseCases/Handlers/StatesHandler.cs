namespace VRisc.UseCases.Handlers;

using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Core.Interfaces;
using VRisc.Infrastructure.Interfaces;

public class StatesHandler(
    IEmulationStatesService states,
    IEmulationStateRepository repo)
{
    public EmulationState Current(string user)
    {
        return states.GetState(user) ?? throw new NotFoundException();
    }

    public EmulationState New(string user)
    {
        var state = new EmulationState(user);
        states.SetState(user, state);

        return state;
    }

    public async Task<EmulationState> Save(string user)
    {
        var state = states.GetState(user) ?? throw new NotFoundException();
        state.Modified = DateTime.Now;

        await repo.StoreState(state);

        return state;
    }

    public void UpdateCurrent(string user, Func<EmulationState, EmulationState> update)
    {
        states.UpdateState(user, state =>
        {
            state = update(state);
            state.Modified = DateTime.Now;
            return state;
        });
    }

    public async Task LoadAsync(string user, string id)
    {
        var state = await repo.LoadState(id);

        states.SetState(user, state);
    }

    public void LoadDram(string user, byte[] dram)
    {
        states.UpdateState(user, state =>
        {
            state.Cpu.Bus.Dram = dram;
            return state;
        });
    }

    public async Task<IEnumerable<EmulationState>> Sessions(string user, uint page, uint size)
    {
        var list = await repo.LoadStates(user);

        return list.Skip((int)(page * size)).Take((int)size);
    }
}