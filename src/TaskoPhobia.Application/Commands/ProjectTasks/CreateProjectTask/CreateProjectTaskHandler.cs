using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;

public class CreateProjectTaskHandler : ICommandHandler<CreateProjectTask>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectTaskHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task HandleAsync(CreateProjectTask command)
    {
        var project = await _projectRepository.FindByIdAsync(command.ProjectId);

        if (project is null || project.OwnerId != command.UserId) throw new ProjectNotFoundException();

        var projectTimeSpan = new TaskTimeSpan(command.Start, command.End);

        var task = new ProjectTask(command.TaskId, command.TaskName, projectTimeSpan, command.ProjectId,
            ProgressStatus.InProgress());
        project.AddTask(task);


        await _projectRepository.UpdateAsync(project);
    }
}