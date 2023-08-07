using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.ProjectTasks.FinishProjectTask;

public sealed record FinishProjectTask(ProjectTaskId ProjectTaskId, ProjectId ProjectId) : ICommand;