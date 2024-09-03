namespace Identity.Api.Middlewares;

using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ValidationException ex)
        {
            var problem = new ValidationProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
            };

            foreach (var error in ex.Errors)
            {
                problem.Errors.Add(error.PropertyName, [error.ErrorMessage]);
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}