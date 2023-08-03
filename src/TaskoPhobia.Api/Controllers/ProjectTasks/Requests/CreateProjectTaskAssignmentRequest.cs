using Microsoft.AspNetCore.Mvc;
using TaskoPhobia.Application.Commands.ProjectTasks.CreateTaskAssignment;

namespace TaskoPhobia.Api.Controllers.ProjectTasks.Requests;

public class RequestBody
{
    public Guid UserId { get; init; }
}

public class CreateProjectTaskAssignmentRequest
{
    [FromBody] public RequestBody Body { get; init; }
    [FromRoute(Name = "projectId")] public Guid ProjectId { get; init; }
    [FromRoute(Name = "taskId")] public Guid TaskId { get; init; }

    public CreateTaskAssignment ToCommand()
    {
        return new CreateTaskAssignment(Guid.NewGuid(), ProjectId, TaskId, Body.UserId);
    }
}