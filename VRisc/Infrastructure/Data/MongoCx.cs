using MongoDB.Driver;
using VRisc.Core.Entities;

namespace VRisc.Infrastructure.Data;

public class MongoCx
{
    private readonly IMongoDatabase _database;

    public MongoCx(string conn, string database)
    {
        var client = new MongoClient(conn);
        _database = client.GetDatabase(database);
    }

    public IMongoCollection<EmulationState> StatesCollection => _database.GetCollection<EmulationState>("States");
}