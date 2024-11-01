namespace VRisc.Infrastructure.Repositories;

using MongoDB.Driver;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class EmulationStateRepository(IMongoCollection<EmulationState> states) : IEmulationStateRepository
{
    public async Task<EmulationState> LoadState(string emulation)
    {
        return await states.Find(state => state.Id == emulation).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<EmulationState>> LoadStates(string user, int page, int size)
    {
        return await states.Find(state => state.User == user).Skip(page * size).Limit(size).ToListAsync();
    }

    public async Task StoreState(EmulationState state)
    {
        var cursor = await states.FindAsync(old => old.Id == state.Id);

        var old = await cursor.FirstOrDefaultAsync();

        state.Creation = old != null ? old.Creation : DateTime.Now;
        state.Modified = DateTime.Now;

        var filter = Builders<EmulationState>.Filter.Eq(x => x.Id, state.Id);

        await states.ReplaceOneAsync(filter, state, new ReplaceOptions { IsUpsert = true });
    }

    public async Task ForgetState(string emulation)
    {
        await states.DeleteOneAsync(state => state.Id == emulation);
    }
}