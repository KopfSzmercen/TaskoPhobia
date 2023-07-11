using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.DomainServices;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.Invitations.CreateInvitation;

internal sealed class CreateInvitationHandler : ICommandHandler<CreateInvitation>
{
    private readonly IClock _clock;
    private readonly IInvitationService _invitationService;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserReadService _userReadService;

    public CreateInvitationHandler(IUserReadService userReadService, IProjectRepository projectRepository, IClock clock,
        IInvitationService invitationService)
    {
        _userReadService = userReadService;
        _projectRepository = projectRepository;
        _clock = clock;
        _invitationService = invitationService;
    }

    public async Task HandleAsync(CreateInvitation command)
    {
        var senderExists = await _userReadService.ExistsByIdAsync(command.SenderId);
        if (!senderExists) throw new UserNotFoundException(command.SenderId);

        var receiverExists = await _userReadService.ExistsByIdAsync(command.ReceiverId);
        if (!receiverExists) throw new UserNotFoundException(command.ReceiverId);

        var project = await _projectRepository.FindByIdAsync(command.ProjectId);
        if (project is null) throw new ProjectNotFoundException();

        var invitation = Invitation.CreateNew(command.InvitationId, $"Invitation for project: {project.Name}",
            command.SenderId, command.ReceiverId, _clock.Now());

        _invitationService.CreateInvitationToProject(project, command.SenderId, command.ReceiverId, invitation);

        await _projectRepository.UpdateAsync(project);
    }
}