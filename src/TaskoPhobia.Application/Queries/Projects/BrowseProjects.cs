using System.ComponentModel;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Projects;

public class BrowseProjects : PagedQuery<ProjectDto>
{
    [DefaultValue(true)] public bool Created { get; set; }

    public string Name { get; set; }
}