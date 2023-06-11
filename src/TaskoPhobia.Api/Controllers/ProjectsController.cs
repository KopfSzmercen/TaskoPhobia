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

namespace TaskoPhobia.Api.Controllers;

[ApiController]
[Route("projects")]
public class ProjectsController : ControllerBase 
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ITokenStorage _tokenStorage;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProjectsController(IQueryDispatcher queryDispatcher, ITokenStorage tokenStorage, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _tokenStorage = tokenStorage;
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation("Create a project")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateProject command)
    {
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();

        command = command with { ProjectId = Guid.NewGuid(), OwnerId = Guid.Parse(currentUserIdStr)};
        await _commandDispatcher.DispatchAsync(command);

        //Temporal nameof(Post)
        return  CreatedAtAction(nameof(Get), new {command.ProjectId}, null);
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation("Get all owned projects")]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> Get()
    {
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();

        var query = new BrowseProjects(Guid.Parse(currentUserIdStr));
        var results = await _queryDispatcher.QueryAsync(query);

        return Ok(results);
    }

    [Authorize]
    [HttpGet("{projectId:guid}")]
    [SwaggerOperation("Get single owned project")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> Get(Guid projectId)
    {
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();

        var query = new GetProject(Guid.Parse(currentUserIdStr), projectId);
        var project = await _queryDispatcher.QueryAsync(query);

        if (project is null) return NotFound();

        return Ok(project);
    }
}