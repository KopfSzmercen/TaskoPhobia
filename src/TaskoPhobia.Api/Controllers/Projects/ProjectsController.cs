using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands.Projects.CreateProject;
using TaskoPhobia.Application.Commands.Projects.FinishProject;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Projects;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers.Projects;

[Authorize]
[Route("projects")]
public class ProjectsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProjectsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    [SwaggerOperation("Create a project")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateProjectRequest request)
    {
        var command = request.ToCommand();
        await _commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), new { command.ProjectId }, null);
    }

    [HttpGet]
    [SwaggerOperation("Get all owned or joined projects projects")]
    [ProducesResponseType(typeof(Paged<ProjectDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<ProjectDto>>> Get([FromQuery] BrowseProjects query)
    {
        var results = await _queryDispatcher.QueryAsync(query);

        return Ok(results);
    }

    [HttpGet("{projectId:guid}")]
    [SwaggerOperation("Get single owned or joined project")]
    [ProducesResponseType(typeof(ProjectDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> Get([FromRoute] Guid projectId)
    {
        var query = new GetProject(projectId);
        var project = await _queryDispatcher.QueryAsync(query);

        if (project is null) return NotFound();

        return Ok(project);
    }

    [HttpPatch("{projectId:guid}/status/finished")]
    [SwaggerOperation("Finish project")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Patch([FromRoute] Guid projectId)
    {
        var command = new FinishProject(projectId);

        await _commandDispatcher.DispatchAsync(command);

        return Ok();
    }
}