using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands.Invitations.AcceptInvitation;
using TaskoPhobia.Application.Commands.Invitations.RejectInvitation;
using TaskoPhobia.Application.Commands.Users.SignIn;
using TaskoPhobia.Application.Commands.Users.SignUp;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Invitations;
using TaskoPhobia.Application.Queries.Users;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers.Users;

[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ITokenStorage _tokenStorage;

    public UsersController(ICommandDispatcher commandDispatcher, ITokenStorage tokenStorage,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _tokenStorage = tokenStorage;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    [SwaggerOperation("Browse users")]
    [ProducesResponseType(typeof(Paged<UserDetailsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<UserDetailsDto>>> Browse([FromQuery] BrowseUsers request)
    {
        var result = await _queryDispatcher.QueryAsync(request);
        return Ok(result);
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Create user account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] SignUpRequest request)
    {
        var command = request.ToCommand();
        await _commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), new { command.UserId }, null);
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in")]
    [ProducesResponseType(typeof(JwtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post([FromBody] SignInRequest request)
    {
        await _commandDispatcher.DispatchAsync(request.ToCommand());
        var jwt = _tokenStorage.Get();
        return Ok(jwt);
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailsDto>> Get()
    {
        var user = await _queryDispatcher.QueryAsync(new GetUser());
        if (user is null) return NotFound();

        return Ok(user);
    }

    [Authorize]
    [HttpGet("me/invitations")]
    [SwaggerOperation("Browse received invitations")]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Paged<ReceivedInvitationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<ReceivedInvitationDto>>> BrowseReceivedInvitations(
        [FromQuery] BrowseReceivedInvitations query)
    {
        var invitations = await _queryDispatcher.QueryAsync(query);
        return Ok(invitations);
    }

    [Authorize]
    [HttpPost("me/invitations/{invitationId:guid}/status/accepted")]
    [SwaggerOperation("Accept invitation and join to the project.")]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromRoute] Guid invitationId)
    {
        var command = new AcceptInvitation(invitationId);
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }

    [Authorize]
    [HttpPatch("me/invitations/{invitationId:guid}/status/rejected")]
    [SwaggerOperation("Reject invitation to project.")]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Patch([FromRoute] Guid invitationId, [FromBody] RejectInvitationRequest request)
    {
        var command = request.ToCommand(invitationId);
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}