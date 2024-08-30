using FluentValidation;
using FluentValidation.AspNetCore;

namespace Identity.Core.Commands;

public static class CommandsValidation
{
    public static IServiceCollection AddCommandsValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<LoginValidation>()
            .AddValidatorsFromAssemblyContaining<RegisterValidation>();

        return services;
    }
}