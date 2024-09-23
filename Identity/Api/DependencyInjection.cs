namespace Identity.Api;

using Microsoft.OpenApi.Models;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddDataValidation();

        services.AddSwaggerGen(c => { c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()); });

        return services;
    }
}