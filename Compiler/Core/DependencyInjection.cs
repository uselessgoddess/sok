using Compiler.Core.Broker;
using Compiler.Core.Compilers.Check;

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
            .AddHostedService<CompileCheckConsumer>()
            .AddScoped<CompileCheckProducer>()
            .AddScoped<ICompileCheck, DummyCheck>()
            .AddScoped<ICompiler, ProcessCompiler>()
            .Decorate<ICompiler, AnalyticsCompiler>()
            .Decorate<ICompiler, CacheCompiler>();
        return builder;
    }
}