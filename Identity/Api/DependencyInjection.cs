namespace Identity.Api;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

        return builder;
    }
}