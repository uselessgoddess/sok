namespace VRisc.Api;

using VRisc.Presentation;
using VRisc.Presentation.Mapper;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(EmulationProfile));
        services.AddSignalR();

        services.AddDataValidation();

        return services;
    }
}