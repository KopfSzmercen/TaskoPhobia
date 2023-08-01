using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;

public class CreateProjectTaskHandler : ICommandHandler<CreateProjectTask>
{
    private readonly IContext _context;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectTaskRepository _projectTaskRepository;

    public CreateProjectTaskHandler(IProjectRepository projectRepository, IContext context,
        IProjectTaskRepository projectTaskRepository)
    {
        _projectRepository = projectRepository;
        _context = context;
        _projectTaskRepository = projectTaskRepository;
    }

    public async Task HandleAsync(CreateProjectTask command)
    {
        var project = await _projectRepository.FindByIdAsync(command.ProjectId);

        if (project is null || project.OwnerId != _context.Identity.Id) throw new ProjectNotFoundException();

        var projectTimeSpan = new TaskTimeSpan(command.Start, command.End);

        var task = ProjectTask.CreateNew(command.TaskId, command.TaskName, projectTimeSpan, project);

        await _projectTaskRepository.AddAsync(task);
    }
}