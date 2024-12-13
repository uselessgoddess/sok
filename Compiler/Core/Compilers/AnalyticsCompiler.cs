namespace Compiler.Core.Compilers;

using System.Diagnostics;
using Compiler.Data.Services;
using Compiler.Core.Interfaces;
using GrpcServices;

public class AnalyticsCompiler(AnalyticsService analytics, ICompiler inner) : ICompiler
{
    public async Task<CompileResponse> Compile(string source, string opt, CancellationToken token = default)
    {
        var instant = new Stopwatch();
        instant.Start();

        var res = await inner.Compile(source, opt, token);

        instant.Stop();


        var len = (uint)source.Length;
        if (res.Success)
        {
            analytics.Success(len, instant.Elapsed.TotalSeconds);
        }
        else
        {
            analytics.Failed(len);
        }

        return res;
    }
}