namespace Identity.Core;

using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Core.Commands;

public static class DataValidation
{
    public static IServiceCollection AddDataValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<LoginValidation>()
            .AddValidatorsFromAssemblyContaining<RegisterValidation>();

        return services;
    }
}