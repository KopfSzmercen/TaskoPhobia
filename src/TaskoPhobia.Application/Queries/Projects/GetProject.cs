using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Projects;

public class GetProject : IQuery<ProjectDetailsDto>
{
    public GetProject(Guid projectId)
    {
        ProjectId = projectId;
    }
    public Guid ProjectId { get; set; }
}