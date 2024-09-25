using System.Runtime.Serialization.Formatters.Binary;
using StackExchange.Redis;

namespace Compiler.Data.Cache;

public class RedisCacheService<T>(IConnectionMultiplexer redis) : ICacheService<T>
{
    public async Task SetCacheAsync(string key, T value)
    {
        await redis.GetDatabase().StringSetAsync(key, SerializeToBytes(value));
    }

    public async Task<T?> GetCacheAsync(string key)
    {
        var val = await redis.GetDatabase().StringGetAsync(key);

        return val.IsNull ? default : DeserializeFromBytes<T>(val);
    }

// BinaryFormatter is safe for this case
#pragma warning disable SYSLIB0011
    private static byte[] SerializeToBytes(object obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    private T? DeserializeFromBytes<T>(byte[] data)
    {
        using (var ms = new MemoryStream(data))
        {
            var formatter = new BinaryFormatter();
            return (T?)formatter.Deserialize(ms);
        }
    }
#pragma warning restore SYSLIB0011
}