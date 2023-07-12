using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.DomainServices;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Invitations.AcceptInvitation;

internal sealed class AcceptInvitationHandler : ICommandHandler<AcceptInvitation>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IInvitationService _invitationService;

    public AcceptInvitationHandler(IInvitationService invitationService, IInvitationRepository invitationRepository)
    {
        _invitationService = invitationService;
        _invitationRepository = invitationRepository;
    }

    public async Task HandleAsync(AcceptInvitation command)
    {
        var invitation = await _invitationRepository.FindByIdAsync(command.InvitationId);
        if (invitation is null || invitation.ReceiverId != command.UserId)
            throw new InvitationNotFoundException(command.InvitationId);

        _invitationService.AcceptInvitationAndJoinProject(invitation);

        await _invitationRepository.UpdateAsync(invitation);
    }
}