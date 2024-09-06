namespace Identity.Api.App.Middlewares;

using Identity.Core;

public class ExceptionsMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            var code = StatusCode(ex);

            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = code,
                ErrorMessage = ex.Message,
            });
        }
    }

    static int StatusCode(Exception ex)
    {
        return ex switch
        {
            BadRequestException => 400,
            UnauthorizedException => 401,
            NotFoundException => 404,
            _ => 500,
        };
    }
}