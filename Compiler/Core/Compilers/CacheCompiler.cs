using Compiler.Core.Interfaces;
using Compiler.Data.Cache;
using GrpcServices;

namespace Compiler.Core.Compilers;

public class CacheCompiler(ICacheService<CompileResponse> cache, ICompiler inner) : ICompiler
{
    public async Task<CompileResponse> Compile(string src, string opt, CancellationToken token = default)
    {
        var key = $"{src}:{opt}";

        var val = await cache.GetCacheAsync(key);

        if (val != null)
        {
            return val;
        }

        var res = await inner.Compile(src, opt, token);

        await cache.SetCacheAsync(key, res);

        return res;
    }
}