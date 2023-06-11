using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands;

public sealed record CreateProjectTask(Guid TaskId, Guid ProjectId, Guid UserId,
    string TaskName, DateTime Start, DateTime End) : ICommand;
