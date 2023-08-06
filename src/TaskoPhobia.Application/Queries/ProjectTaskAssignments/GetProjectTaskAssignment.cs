using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.ProjectTaskAssignments;

public class GetProjectTaskAssignment : IQuery<ProjectTaskAssignmentDto>
{
    public GetProjectTaskAssignment(Guid projectId, Guid projectTaskId, Guid projectTaskAssignmentId)
    {
        ProjectId = projectId;
        ProjectTaskId = projectTaskId;
        ProjectTaskAssignmentId = projectTaskAssignmentId;
    }

    public Guid ProjectId { get; init; }
    public Guid ProjectTaskId { get; init; }
    public Guid ProjectTaskAssignmentId { get; init; }
}