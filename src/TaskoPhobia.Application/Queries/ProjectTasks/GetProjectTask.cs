using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.ProjectTasks;

public class GetProjectTask : IQuery<ProjectTaskDetailsDto>
{
    public GetProjectTask(Guid projectTaskId, Guid projectId)
    {
        ProjectTaskId = projectTaskId;
        ProjectId = projectId;
    }

    public Guid ProjectTaskId { get; set; }
    public Guid ProjectId { get; set; }
}