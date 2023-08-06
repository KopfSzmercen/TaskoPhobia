using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Api.Attributes;
using TaskoPhobia.Api.Controllers.ProjectTasks.Requests;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers.ProjectTasks;

[Route("projects/{projectId:guid}/tasks/{taskId:guid}/assignments")]
[Authorize]
public class ProjectTaskAssignmentsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProjectTaskAssignmentsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [SwaggerOperation("Create project task assignments")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromMultiSource] CreateProjectTaskAssignmentRequest request)
    {
        var command = request.ToCommand();
        await _commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get),
            new { projectId = request.ProjectId, taskId = request.TaskId, assignmentId = command.AssignmentId },
            null);
    }

    [HttpGet("{assignmentId:guid}")]
    [SwaggerOperation("Get project task assignment")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProjectTaskAssignmentDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProjectTaskAssignmentDto>> Get(
        [FromMultiSource] GetProjectTaskAssignmentRequest request)
    {
        var query = request.ToQuery();
        var result = await _queryDispatcher.QueryAsync(query);

        if (result is null) return NotFound();

        return Ok(result);
    }
}