using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Projects;

public class BrowseProjects : IQuery<IEnumerable<ProjectDto>>
{
    public BrowseProjects(Guid userId, bool created)
    {
        UserId = userId;
        Created = created;
    }

    public Guid UserId { get; set; }
    public bool Created { get; set; }
}