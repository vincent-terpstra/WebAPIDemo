using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIDemo.Filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionHandlerFilter> _logger;

    public ExceptionHandlerFilter(ILogger<ExceptionHandlerFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case KeyNotFoundException:
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                break;
            default:
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                _logger.LogError(context.Exception, "Unhandled Exception");
                break;
        }
    }
}