using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.ProjectTasks;

[ApiController]
[Route("projects/{projectId:guid}/tasks")]
[Authorize]
public class ProjectTasksController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
// #CR jak wcześniej
    public ProjectTasksController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [SwaggerOperation("Create project task")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateProjectTaskRequest request, [FromRoute]Guid projectId)
    {
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();
        var command = request.ToCommand(Guid.Parse(currentUserIdStr), projectId);

        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new {projectId, projectTaskId = command.TaskId}, null);

    }
    
    [HttpGet]
    [SwaggerOperation("Get list of project tasks (no pagination so far)")]
    [ProducesResponseType(typeof(IEnumerable<ProjectTaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProjectTaskDto>>>Get([FromRoute]Guid projectId)
    {
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();
        var query = new BrowseProjectTasks(Guid.Parse(currentUserIdStr), projectId);

        var result = await _queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [HttpGet("{projectTaskId:guid}")]
    [SwaggerOperation("Get project task")]
    [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectTaskDto>> Get([FromRoute]Guid projectId, [FromRoute]Guid projectTaskId)
    {
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();
        var query = new GetProjectTask(projectTaskId, projectId, Guid.Parse(currentUserIdStr));

        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null) return NotFound();

        return Ok(result);
    }

}