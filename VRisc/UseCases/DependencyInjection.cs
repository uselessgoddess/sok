using Microsoft.Extensions.DependencyInjection;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Notifiers;

namespace VRisc.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services
            .AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
            .AddScoped<ICheckNotifier, HubNotifier>();
        return services;
    }
}