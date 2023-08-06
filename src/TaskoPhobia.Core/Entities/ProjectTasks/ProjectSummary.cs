using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities.ProjectTasks;

public class ProjectSummary
{
    public ProjectId Id { get; init; }
    public ProgressStatus Status { get; init; }
    public UserId OwnerId { get; init; }
}