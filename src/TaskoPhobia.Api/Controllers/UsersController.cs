
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase

{
    private readonly ICommandDispatcher _commandDispatcher;

    public UsersController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    [SwaggerOperation("Create user account")]
    [ProducesResponseType(typeof(void),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _commandDispatcher.DispatchAsync(command);
        
        return  CreatedAtAction(nameof(Get), new {command.UserId}, null);
    }

    [HttpGet]
    public ActionResult Get()
    {
        return Ok();
    }
}