using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.ProjectTasks.FinishProjectTask;

internal sealed class FinishProjectTaskHandler : ICommandHandler<FinishProjectTask>
{
    private readonly IContext _context;
    private readonly IProjectTaskRepository _projectTaskRepository;

    public FinishProjectTaskHandler(IProjectTaskRepository projectTaskRepository, IContext context)
    {
        _projectTaskRepository = projectTaskRepository;
        _context = context;
    }

    public async Task HandleAsync(FinishProjectTask command)
    {
        var projectTask = await _projectTaskRepository.FindByIdAsync(command.ProjectTaskId);

        if (projectTask is null || projectTask.ProjectId != command.ProjectId) throw new ProjectTaskNotFoundException();

        projectTask.Finish(_context.Identity.Id);

        await _projectTaskRepository.UpdateAsync(projectTask);
    }
}