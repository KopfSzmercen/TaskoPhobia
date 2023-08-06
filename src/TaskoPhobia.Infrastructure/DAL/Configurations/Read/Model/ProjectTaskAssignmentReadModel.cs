namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class ProjectTaskAssignmentReadModel
{
    public Guid Id { get; init; }
    public UserReadModel User { get; init; }
    public Guid AssigneeId { get; init; }
    public Guid TaskId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public ProjectTaskReadModel ProjectTask { get; init; }
}