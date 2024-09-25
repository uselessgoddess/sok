using Compiler.Core.Interfaces;
using Compiler.Data.Cache;
using Google.Protobuf;
using GrpcServices;

namespace Compiler.Core.Compilers;

public class CacheCompiler(ICacheService cache, ICompiler inner) : ICompiler
{
    public async Task<CompileResponse> Compile(string src, string opt, CancellationToken token = default)
    {
        var key = $"{src}:{opt}";

        var val = await cache.GetCacheAsync(key);

        if (val != null)
        {
            return CompileResponse.Parser.ParseFrom(val);
        }

        var res = await inner.Compile(src, opt, token);

        await cache.SetCacheAsync(key, res.ToByteArray());

        return res;
    }
}