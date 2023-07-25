using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.Projects.CreateProject;

internal sealed class CreateProjectHandler : ICommandHandler<CreateProject>
{
    private readonly IClock _clock;
    private readonly IContext _context;
    private readonly IUserRepository _userRepository;

    public CreateProjectHandler(IUserRepository userRepository, IClock clock, IContext context)
    {
        _userRepository = userRepository;
        _clock = clock;
        _context = context;
    }

    public async Task HandleAsync(CreateProject command)
    {
        var ownerId = _context.Identity.Id;

        var user = await _userRepository.GetByIdAsync(ownerId);

        if (user is null) throw new UserNotFoundException(ownerId);

        var project = Project.CreateNew(command.ProjectId, command.ProjectName, command.ProjectDescription, _clock,
            ownerId);

        user.AddProject(project);

        await _userRepository.UpdateAsync(user);
    }
}