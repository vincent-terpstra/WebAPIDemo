namespace WebAPIDemo.Filters;

public class KeyNotFoundMiddleware
{
    private readonly RequestDelegate _request;

    public KeyNotFoundMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (KeyNotFoundException exception)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(exception.Message);
        }
    }
}