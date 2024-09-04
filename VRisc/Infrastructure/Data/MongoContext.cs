using MongoDB.Driver;
using VRisc.Core.Entities;

namespace VRisc.Infrastructure.Data;

public class MongoContext(MongoClient client, string database)
{
    private readonly IMongoDatabase _database = client.GetDatabase(database);

    public IMongoCollection<EmulationState> StatesCollection => _database.GetCollection<EmulationState>("States");
}