using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIDemo.Filters;

public class ModelStateValidationAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid)
        {
            await next.Invoke();
        }
        else
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}