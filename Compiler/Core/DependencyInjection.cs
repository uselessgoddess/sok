namespace Core;

using Core.Compilers;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICompiler, ProcessCompiler>();
        return builder;
    }
}