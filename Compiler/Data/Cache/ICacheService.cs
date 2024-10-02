namespace Compiler.Data.Cache;

public interface ICacheService
{
    Task SetCacheAsync(string key, byte[] value);

    Task<byte[]?> GetCacheAsync(string key);

    void ClearCache();
}