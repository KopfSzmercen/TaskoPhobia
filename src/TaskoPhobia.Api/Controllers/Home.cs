using Microsoft.AspNetCore.Mvc;

namespace TaskoPhobia.Api.Controllers;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{
    // #CR niepotrzebny home controller :D
    [HttpGet]
    public ActionResult<String> Get()
    {
        return Ok("Hello");
    }
}
