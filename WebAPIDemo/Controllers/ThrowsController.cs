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
    
    [HttpGet]
    [Route("/api/query")]
    public async Task<ActionResult<QueryModel>> GetQuery([FromQuery] string queryString, [FromQuery] int queryInt)
    {
        QueryModel obj = new()
        {
            Query = queryString,
            Value = queryInt
        };

        return Ok(obj);
    }
}