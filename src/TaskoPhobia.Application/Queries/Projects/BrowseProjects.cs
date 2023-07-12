using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Projects;

public class BrowseProjects : IQuery<IEnumerable<ProjectDto>>
{
    public BrowseProjects(Guid userId, bool takeJoined)
    {
        UserId = userId;
        TakeJoined = takeJoined;
    }

    public Guid UserId { get; set; }
    public bool TakeJoined { get; set; }
}