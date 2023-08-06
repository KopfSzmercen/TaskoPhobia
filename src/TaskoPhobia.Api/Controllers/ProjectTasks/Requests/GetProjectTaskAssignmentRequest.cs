using Microsoft.AspNetCore.Mvc;
using TaskoPhobia.Application.Queries.ProjectTaskAssignments;

namespace TaskoPhobia.Api.Controllers.ProjectTasks.Requests;

public class GetProjectTaskAssignmentRequest
{
    [FromRoute(Name = "projectId")] public Guid ProjectId { get; init; }
    [FromRoute(Name = "taskId")] public Guid TaskId { get; init; }
    [FromRoute(Name = "assignmentId")] public Guid AssignmentId { get; init; }

    public GetProjectTaskAssignment ToQuery()
    {
        return new GetProjectTaskAssignment(ProjectId, TaskId, AssignmentId);
    }
}