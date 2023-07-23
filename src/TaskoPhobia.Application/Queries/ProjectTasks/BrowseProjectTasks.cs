using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.ProjectTasks;

public class BrowseProjectTasks : PagedQuery<ProjectTaskDto>
{
    public BrowseProjectTasks(Guid projectId)
    {
        ProjectId = projectId;
    }

    public Guid ProjectId { get; }
}