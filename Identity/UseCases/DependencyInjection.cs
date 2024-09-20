using Identity.Core.Interfaces;
using Identity.UseCases.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
        );
        
        return services;
    }
}