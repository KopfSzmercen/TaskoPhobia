using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Users;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase

{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ITokenStorage _tokenStorage;
    private readonly IQueryDispatcher _queryDispatcher;

    public UsersController(ICommandDispatcher commandDispatcher, ITokenStorage tokenStorage, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _tokenStorage = tokenStorage;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Create user account")]
    [ProducesResponseType(typeof(void),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] SignUpRequest request)
    {
        var command = request.ToCommand();
        await _commandDispatcher.DispatchAsync(command);
        
        return  CreatedAtAction(nameof(Get), new {command.UserId}, null);
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in")]
    [ProducesResponseType(typeof(JwtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(SignInRequest request)
    {
        await _commandDispatcher.DispatchAsync(request.ToCommand());
        var jwt = _tokenStorage.Get();
        return Ok(jwt);
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity.Name);
        var user = await _queryDispatcher.QueryAsync(new GetUser { UserId = userId });
        
        if (user is null)  return NotFound();

        return Ok(user);
    }
}