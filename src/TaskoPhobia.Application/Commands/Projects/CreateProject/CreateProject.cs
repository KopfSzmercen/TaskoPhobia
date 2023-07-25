using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Projects.CreateProject;

public record CreateProject(Guid ProjectId, string ProjectName, string ProjectDescription) : ICommand;