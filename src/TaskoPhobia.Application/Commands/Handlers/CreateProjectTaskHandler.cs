using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Handlers;

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

        if (project is null || project.OwnerId.Value != command.UserId) throw new ProjectNotFoundException();

        var projectTaskId = new ProjectTaskId(command.TaskId);
        var projectTaskName = new ProjectTaskName(command.TaskName);
        var projectTimeSpan = new TaskTimeSpan(command.Start, command.End);
        var projectId = new ProjectId(command.ProjectId);
        
        var task = new ProjectTask(projectTaskId, projectTaskName, projectTimeSpan, projectId, ProgressStatus.InProgress());
        project.AddTask(task);
 
        
        await _projectRepository.UpdateAsync(project);
    }
}