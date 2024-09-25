namespace Compiler.Data.Cache;

public interface ICacheService<T>
{
    Task SetCacheAsync(string key, T value);

    Task<T?> GetCacheAsync(string key);
}