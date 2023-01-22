namespace WebAPIDemo.Filters;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _request;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate request, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _request = request;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(exception.Message);
        }
    }
}