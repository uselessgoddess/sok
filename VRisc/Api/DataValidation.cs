namespace VRisc.Presentation;

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using VRisc.Presentation.Validations;

public static class DataValidation
{
    public static IServiceCollection AddDataValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<EmulationStateDtoValidation>();

        return services;
    }
}