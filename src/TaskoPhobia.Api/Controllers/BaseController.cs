
using Microsoft.AspNetCore.Mvc;


namespace TaskoPhobia.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected Guid GetUserId()
    {
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr)) throw new Exception("Can't access user id.");
        return Guid.Parse(currentUserIdStr);
    }
}