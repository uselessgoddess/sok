using MongoDB.Driver;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

namespace VRisc.Infrastructure.Repositories;

public class EmulationStateRepository(IMongoCollection<EmulationState> states) : IEmulationStateRepository
{
    public async Task<EmulationState> LoadStateAsync(string emulation)
    {
        return await states.Find(state => state.Id == emulation).FirstAsync();
    }

    public async Task<IAsyncCursor<EmulationState>> LoadStatesAsync(string user)
    {
        return await states.FindAsync(state => state.User == user);
    }

    public async Task StoreStateAsync(EmulationState state)
    {
        await states.InsertOneAsync(state);
    }

    public async Task ForgetStateAsync(string emulation)
    {
        await states.DeleteOneAsync(state => state.Id == emulation);
    }
}