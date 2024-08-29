using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Identity.Controllers;

public class WalidateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}

public static class HelperExtension
{
    public static async Task<IActionResult> RouteEmpty(this IRequest req, IMediator mediator)
    {
        return await RouteImpl(async () =>
        {
            await mediator.Send(req);
            return new EmptyResult();
        });
    }

    public static async Task<IActionResult> Route<T>(this IRequest<T> req, IMediator mediator)
    {
        return await RouteImpl(async () => await mediator.Send(req));
    }

    static async Task<IActionResult> RouteImpl<T>(Func<Task<T>> impl)
    {
        T? result;
        try
        {
            result = await impl();
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

            return new BadRequestObjectResult(problem);
        }
        catch (Exception ex)
        {
            return ex switch
            {
                BadRequestException => new BadRequestObjectResult(ex.Message),
                UnauthorizedException => new UnauthorizedObjectResult(ex.Message),
                NotFoundException => new NotFoundObjectResult(ex.Message),
                _ => throw ex
            };
        }

        return new OkObjectResult(result);
    }
}