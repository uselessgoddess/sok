using Identity.Api.App.Middlewares;

namespace Identity.Api.Middlewares;

public static class Middlewares
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsMiddleware>().UseMiddleware<ValidationMiddleware>();
        return app;
    }
}