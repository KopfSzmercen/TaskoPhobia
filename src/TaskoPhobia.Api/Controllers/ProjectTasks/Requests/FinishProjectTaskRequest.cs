using Microsoft.AspNetCore.Mvc;
using TaskoPhobia.Application.Commands.ProjectTasks.FinishProjectTask;

namespace TaskoPhobia.Api.Controllers.ProjectTasks.Requests;

public class FinishProjectTaskRequest
{
    [FromRoute(Name = "projectId")] public Guid ProjectId { get; init; }
    [FromRoute(Name = "taskId")] public Guid TaskId { get; init; }

    public FinishProjectTask ToCommand()
    {
        return new FinishProjectTask(TaskId, ProjectId);
    }
}