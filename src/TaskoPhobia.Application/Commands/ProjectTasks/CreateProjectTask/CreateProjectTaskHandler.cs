using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;

public class CreateProjectTaskHandler : ICommandHandler<CreateProjectTask>
{
    private readonly IContext _context;
    private readonly IProjectRepository _projectRepository;

    public CreateProjectTaskHandler(IProjectRepository projectRepository, IContext context)
    {
        _projectRepository = projectRepository;
        _context = context;
    }

    public async Task HandleAsync(CreateProjectTask command)
    {
        var project = await _projectRepository.FindByIdAsync(command.ProjectId);

        if (project is null || project.OwnerId != _context.Identity.Id) throw new ProjectNotFoundException();

        var projectTimeSpan = new TaskTimeSpan(command.Start, command.End);

        var task = ProjectTask.CreateNew(command.TaskId, command.TaskName, projectTimeSpan, command.ProjectId);

        project.AddTask(task);

        await _projectRepository.UpdateAsync(project);
    }
}