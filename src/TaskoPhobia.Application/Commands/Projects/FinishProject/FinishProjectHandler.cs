using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.Projects.FinishProject;

internal sealed class FinishProjectHandler : ICommandHandler<FinishProject>
{
    private readonly IContext _context;
    private readonly IProjectReadService _projectReadService;
    private readonly IProjectRepository _projectRepository;

    public FinishProjectHandler(IContext context, IProjectRepository projectRepository,
        IProjectReadService projectReadService)
    {
        _context = context;
        _projectRepository = projectRepository;
        _projectReadService = projectReadService;
    }

    public async Task HandleAsync(FinishProject command)
    {
        var project = await _projectRepository.FindByIdAsync(command.ProjectId);
        if (project is null) throw new ProjectNotFoundException();

        var allProjectTasksAreFinished = await _projectReadService.CheckAllTasksAreFinishedAsync(command.ProjectId);
        project.Finish(_context.Identity.Id, allProjectTasksAreFinished);

        await _projectRepository.UpdateAsync(project);
    }
}