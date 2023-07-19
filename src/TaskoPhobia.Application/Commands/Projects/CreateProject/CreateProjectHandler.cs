using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.Projects.CreateProject;

internal sealed class CreateProjectHandler : ICommandHandler<CreateProject>
{
    private readonly IClock _clock;
    private readonly IUserRepository _userRepository;

    public CreateProjectHandler(IUserRepository userRepository, IClock clock)
    {
        _userRepository = userRepository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateProject command)
    {
        var ownerId = new UserId(command.OwnerId);
        var user = await _userRepository.GetByIdAsync(ownerId);

        if (user is null) throw new UserNotFoundException(ownerId);

        var projectProgressStatus = ProgressStatus.InProgress();

        var project = new Project(command.ProjectId, command.ProjectName, command.ProjectDescription,
            projectProgressStatus, _clock.Now(), ownerId);

        user.AddProject(project);

        await _userRepository.UpdateAsync(user);
    }
}