using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Projects.FinishProject;

public record FinishProject(ProjectId ProjectId) : ICommand;