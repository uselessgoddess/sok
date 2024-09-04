using VRisc.Presentation.Mapper;

namespace VRisc.Presentation;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(EmulationProfile));
        services.AddSignalR();

        return services;
    }
}