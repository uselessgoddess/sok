namespace Compiler.Core;

using Compiler.Core.Interfaces;
using Compiler.Core.Compilers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddCore(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<ICompiler, ProcessCompiler>()
            .AddScoped<ICompiler, AnalyticsCompiler>()
            .AddScoped<ICompiler, CacheCompiler>();
        return builder;
    }
}