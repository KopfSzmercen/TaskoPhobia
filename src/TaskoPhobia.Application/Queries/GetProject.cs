using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries;

public class GetProject : IQuery<ProjectDto>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }

    public GetProject(Guid userId, Guid projectId)
    {
        UserId = userId;
        ProjectId = projectId;
    }
}