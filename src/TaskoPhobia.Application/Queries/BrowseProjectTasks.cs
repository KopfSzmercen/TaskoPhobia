using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries;

public class BrowseProjectTasks: IQuery<IEnumerable<ProjectTaskDto>>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }

    public BrowseProjectTasks(Guid userId, Guid projectId)
    {
        UserId = userId;
        ProjectId = projectId;
    }
}