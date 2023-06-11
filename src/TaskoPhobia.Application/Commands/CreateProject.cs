

using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands;

public record CreateProject(Guid ProjectId, string ProjectName, string ProjectDescription, Guid OwnerId) : ICommand;