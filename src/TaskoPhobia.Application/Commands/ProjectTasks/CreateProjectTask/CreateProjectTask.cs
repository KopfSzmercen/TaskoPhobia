using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;

public sealed record CreateProjectTask(Guid TaskId, Guid ProjectId,
    string TaskName, DateTime Start, DateTime End) : ICommand
{
}