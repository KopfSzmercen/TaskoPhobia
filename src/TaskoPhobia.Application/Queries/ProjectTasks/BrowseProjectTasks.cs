using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.ProjectTasks;

public class BrowseProjectTasks : PagedQuery<ProjectTaskDto>
{
    public Guid ProjectId { get; set; }
}