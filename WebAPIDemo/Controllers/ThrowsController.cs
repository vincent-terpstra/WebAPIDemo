using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers;


[Route("/api/throws")]
public class ThrowsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        throw new Exception("Whoops");
    }
}