using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries;

public class BrowseProjects : IQuery<IEnumerable<ProjectDto>>
{
    public BrowseProjects(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}