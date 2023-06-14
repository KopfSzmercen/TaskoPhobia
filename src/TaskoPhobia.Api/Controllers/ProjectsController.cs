using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands.Projects.CreateProject;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers;


[Route("projects")]
public class ProjectsController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProjectsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation("Create a project")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateProjectRequest request)
    {
        var command = request.ToCommand(GetUserId());
        await _commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), new { command.ProjectId }, null);
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation("Get all owned projects")]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> Get()
    {
        var query = new BrowseProjects(GetUserId());
        var results = await _queryDispatcher.QueryAsync(query);

        return Ok(results);
    }

    [Authorize]
    [HttpGet("{projectId:guid}")]
    [SwaggerOperation("Get single owned project")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> Get([FromRoute] Guid projectId)
    {
       

        var query = new GetProject(GetUserId(), projectId);
        var project = await _queryDispatcher.QueryAsync(query);

        if (project is null) return NotFound();

        return Ok(project);
    }
}