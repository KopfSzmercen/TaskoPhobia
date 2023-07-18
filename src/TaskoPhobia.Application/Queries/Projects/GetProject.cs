using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Projects;

public class GetProject : IQuery<ProjectDetailsDto>
{
    public GetProject(Guid userId, Guid projectId)
    {
        UserId = userId;
        ProjectId = projectId;
    }

    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
}