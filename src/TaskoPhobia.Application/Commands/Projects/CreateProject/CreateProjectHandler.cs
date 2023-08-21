using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.DomainServices.Projects;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.Projects.CreateProject;

internal sealed class CreateProjectHandler : ICommandHandler<CreateProject>
{
    private readonly IClock _clock;
    private readonly IContext _context;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectService _projectService;
    private readonly IUserRepository _userRepository;

    public CreateProjectHandler(IUserRepository userRepository, IClock clock, IContext context,
        IProjectService projectService, IProjectRepository projectRepository)
    {
        _userRepository = userRepository;
        _clock = clock;
        _context = context;
        _projectService = projectService;
        _projectRepository = projectRepository;
    }

    public async Task HandleAsync(CreateProject command)
    {
        var ownerId = _context.Identity.Id;

        var user = await _userRepository.FindByIdAsync(ownerId);

        if (user is null) throw new UserNotFoundException(ownerId);

        var project =
            await _projectService.CreateProject(command.ProjectId, command.ProjectName, command.ProjectDescription,
                user);

        await _projectRepository.AddAsync(project);
    }
}