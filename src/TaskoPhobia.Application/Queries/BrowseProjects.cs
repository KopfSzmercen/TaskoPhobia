using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries;

public class BrowseProjects : IQuery<IEnumerable<ProjectDto>>
{
    public Guid UserId { get; set; }

    public BrowseProjects(Guid userId)
    {
        UserId = userId;
    }
}