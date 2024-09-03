namespace Identity.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
        return new OkObjectResult(await impl());
    }
}