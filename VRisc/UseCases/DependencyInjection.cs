namespace VRisc.UseCases;

using Microsoft.Extensions.DependencyInjection;
using VRisc.UseCases.Broker;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Notifiers;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services
            .AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
            .AddHostedService<CompileCheckConsumer>()
            .AddScoped<CompileCheckProducer>()
            .AddScoped<ICheckNotifier, HubNotifier>();
        return services;
    }
}