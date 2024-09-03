namespace Identity.Api;

using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Api.Commands;
using Identity.Api.Validations;

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