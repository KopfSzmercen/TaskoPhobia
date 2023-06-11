using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries;

public class GetProjectTask : IQuery<ProjectTaskDto>
{
    public Guid ProjectTaskId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    public GetProjectTask(Guid projectTaskId, Guid projectId, Guid userId)
    {
        ProjectTaskId = projectTaskId;
        ProjectId = projectId;
        UserId = userId;
    }
}