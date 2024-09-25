using System.Runtime.Serialization.Formatters.Binary;
using StackExchange.Redis;

namespace Compiler.Data.Cache;

public class RedisCacheService(IConnectionMultiplexer redis) : ICacheService
{
    public async Task SetCacheAsync(string key, byte[] value)
    {
        await redis.GetDatabase().StringSetAsync(key, value);
    }

    public async Task<byte[]?> GetCacheAsync(string key)
    {
        var val = await redis.GetDatabase().StringGetAsync(key);

        return val.IsNull ? default : val;
    }
}