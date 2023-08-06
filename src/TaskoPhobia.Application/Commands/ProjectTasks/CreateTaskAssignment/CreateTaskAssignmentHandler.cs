using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateTaskAssignment;

public class CreateTaskAssignmentHandler : ICommandHandler<CreateTaskAssignment>
{
    private readonly IContext _context;
    private readonly IProjectParticipationReadService _projectParticipationReadService;
    private readonly IProjectTaskRepository _projectTaskRepository;

    public CreateTaskAssignmentHandler(
        IProjectTaskRepository projectTaskRepository, IContext context,
        IProjectParticipationReadService projectParticipationReadService)
    {
        _projectTaskRepository = projectTaskRepository;
        _context = context;
        _projectParticipationReadService = projectParticipationReadService;
    }

    public async Task HandleAsync(CreateTaskAssignment command)
    {
        var projectTask = await _projectTaskRepository.FindByIdAsync(command.ProjectTaskId);
        if (projectTask is null) throw new ProjectTaskNotFoundException();

        var currentUserIsProjectOwner = projectTask.Project.OwnerId.Equals(_context.Identity.Id);

        if (!projectTask.ProjectId.Equals(command.ProjectId) || !currentUserIsProjectOwner)
            throw new ProjectTaskNotFoundException();

        var assigneeParticipatesProject =
            await _projectParticipationReadService.IsUserProjectParticipantAsync(projectTask.ProjectId,
                command.AssigneeId);

        projectTask.AddAssignee(command.AssignmentId, command.AssigneeId, assigneeParticipatesProject);

        await _projectTaskRepository.UpdateAsync(projectTask);
    }
}