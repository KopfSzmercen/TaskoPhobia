using Microsoft.AspNetCore.Mvc;

namespace TaskoPhobia.Api.Controllers;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{

    [HttpGet]
    public ActionResult<String> Get()
    {
        return Ok("Hello");
    }
}
