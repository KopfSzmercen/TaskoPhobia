using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.ProjectTasks;

public class GetProjectTask : IQuery<ProjectTaskDto>
{
    public GetProjectTask(Guid projectTaskId, Guid projectId, Guid userId)
    {
        ProjectTaskId = projectTaskId;
        ProjectId = projectId;
        UserId = userId;
    }

    public Guid ProjectTaskId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
}