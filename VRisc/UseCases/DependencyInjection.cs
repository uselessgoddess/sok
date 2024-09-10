namespace VRisc.UseCases;

using Microsoft.Extensions.DependencyInjection;
using VRisc.Core.Interfaces;
using VRisc.Infrastructure.Repositories;
using VRisc.UseCases.Emulation;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        {
            services.AddSingleton<IEmulationStatesService, EmulationStatesService>();
            services.AddSingleton<IEmulationTaskManager, EmulationTaskManger>();
            services.AddSingleton<IEmulator, DummyEmulator>();

            services.AddScoped<IEmulationStateRepository, EmulationStateRepository>();
        }

        return services;
    }
}