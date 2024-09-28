namespace VRisc.UseCases;

using Microsoft.Extensions.DependencyInjection;
using VRisc.UseCases.Broker;
using VRisc.UseCases.Handlers;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services
            .AddHostedService<CompileCheckConsumer>()
            .AddScoped<TasksHandler>()
            .AddScoped<StatesHandler>();
        return services;
    }
}