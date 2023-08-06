using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateTaskAssignment;

public sealed record CreateTaskAssignment
    (Guid AssignmentId, Guid ProjectId, Guid ProjectTaskId, Guid AssigneeId) : ICommand;