namespace VRisc.Api.Middlewares;

using VRisc.Core.Exceptions;

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

            if (code == 0)
            {
                throw;
            }

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
            AlreadyExistsException => 403,
            NotFoundException => 404,
            TimeoutException => 408,
            _ => 0,
        };
    }
}