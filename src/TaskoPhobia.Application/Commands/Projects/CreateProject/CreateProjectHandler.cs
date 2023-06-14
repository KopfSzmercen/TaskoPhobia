using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Projects.CreateProject;

internal sealed class CreateProjectHandler : ICommandHandler<CreateProject>
{
    private readonly IUserRepository _userRepository;
    public CreateProjectHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task HandleAsync(CreateProject command)
    {
        var ownerId = new UserId(command.OwnerId);
        var user = await _userRepository.GetByIdAsync(ownerId);

        if (user is null) throw new UserNotFoundException();
        
        var projectProgressStatus = ProgressStatus.InProgress();
        
        var project = new Project(command.ProjectId, command.ProjectName, command.ProjectDescription, 
            projectProgressStatus, DateTime.UtcNow, ownerId);

        user.AddProject(project);

        await _userRepository.UpdateAsync(user);
    }
}