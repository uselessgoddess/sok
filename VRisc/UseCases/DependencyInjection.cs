namespace VRisc.UseCases;

using Microsoft.Extensions.DependencyInjection;
using VRisc.UseCases.Broker;
using VRisc.UseCases.Handlers;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Notifiers;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services
            .AddHostedService<CompileCheckConsumer>()
            .AddScoped<CompileCheckProducer>()
            .AddScoped<ICheckNotifier, HubNotifier>()
            .AddScoped<TasksHandler>()
            .AddScoped<StatesHandler>();
        return services;
    }
}