namespace Identity.Api.Middlewares;

using Identity.Api.App.Middlewares;

public static class Middlewares
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsMiddleware>();
        return app;
    }
}