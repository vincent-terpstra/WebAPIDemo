using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers;

public class BaseController<T> : ControllerBase where T : BaseController<T>
{
    private ILogger<T>? _logger;
    private IMediator? _mediator;

    protected ILogger<T> Logger => 
        _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();


}