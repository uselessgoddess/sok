using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
    public static Router<T> Route<T>(this IRequest<T> req, IMediator mediator)
    {
        return new Router<T>(mediator.Send(req)!);
    }
}

public class Router<T>(Task<T?> task)
{
    private Func<T, IActionResult> _then = x => new OkObjectResult(x);
    private Func<IActionResult> _else = () => new BadRequestResult();

    public Router<T> Then(Func<T, IActionResult> then)
    {
        _then = then;
        return this;
    }

    public Router<T> Else(Func<IActionResult> @else)
    {
        _else = @else;
        return this;
    }

    public async Task<IActionResult> Result()
    {
        var result = await task;
        return result != null ? _then.Invoke(result) : _else.Invoke();
    }
}