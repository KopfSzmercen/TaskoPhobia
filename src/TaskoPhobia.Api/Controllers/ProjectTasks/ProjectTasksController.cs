using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Api.Attributes;
using TaskoPhobia.Api.Controllers.ProjectTasks.Requests;
using TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.ProjectTasks;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers.ProjectTasks;

[Route("projects/{projectId:guid}/tasks")]
[Authorize]
public class ProjectTasksController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProjectTasksController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [SwaggerOperation("Create project task")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateProjectTaskRequest request, [FromRoute] Guid projectId)
    {
        var command = request.ToCommand(projectId);

        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new { projectId, projectTaskId = command.TaskId }, null);
    }

    [HttpGet]
    [SwaggerOperation("Get list of project tasks")]
    [ProducesResponseType(typeof(Paged<ProjectTaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Paged<ProjectTaskDto>>> Get([FromMultiSource] BrowseProjectTasksRequest request)
    {
        var query = new BrowseProjectTasks
        {
            Page = request.Page,
            Results = request.Results,
            OrderBy = request.OrderBy,
            SortOrder = request.SortOrder,
            ProjectId = request.ProjectId
        };

        var result = await _queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [HttpGet("{projectTaskId:guid}")]
    [SwaggerOperation("Get project task")]
    [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectTaskDto>> Get([FromRoute] Guid projectId, [FromRoute] Guid projectTaskId)
    {
        var query = new GetProjectTask(projectTaskId, projectId);

        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null) return NotFound();

        return Ok(result);
    }
}