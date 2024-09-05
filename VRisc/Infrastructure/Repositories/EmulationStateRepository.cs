namespace VRisc.Infrastructure.Repositories;

using MongoDB.Driver;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class EmulationStateRepository(IMongoCollection<EmulationState> states) : IEmulationStateRepository
{
    public async Task<EmulationState> LoadState(string emulation)
    {
        return await states.Find(state => state.Id == emulation).FirstAsync();
    }

    public Task<IEnumerable<EmulationState>> LoadStates(string user)
    {
        return Task.FromResult(states.Find(state => state.User == user).ToEnumerable());
    }

    public async Task StoreState(EmulationState state)
    {
        await states.InsertOneAsync(state);
    }

    public async Task ForgetState(string emulation)
    {
        await states.DeleteOneAsync(state => state.Id == emulation);
    }
}