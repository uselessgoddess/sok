namespace VRisc.Infrastructure.Data;

using MongoDB.Driver;
using VRisc.Core.Entities;

public class MongoContext(MongoClient client, string database)
{
    private readonly IMongoDatabase database = client.GetDatabase(database);

    public IMongoCollection<EmulationState> StatesCollection => database.GetCollection<EmulationState>("States");
}