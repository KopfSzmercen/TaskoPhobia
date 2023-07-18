using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Invitations.RejectInvitation;

public class RejectInvitationHandler : ICommandHandler<RejectInvitation>
{
    private readonly IInvitationRepository _invitationRepository;

    public RejectInvitationHandler(IInvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository;
    }

    public async Task HandleAsync(RejectInvitation command)
    {
        var invitation = await _invitationRepository.FindByIdAsync(command.InvitationId);
        if (invitation is null) throw new InvitationNotFoundException(command.InvitationId);

        if (invitation.ReceiverId != command.ReceiverId) throw new NotAllowedToRejectInvitationException();

        invitation.Reject(command.blockSendingMoreInvitations);

        await _invitationRepository.UpdateAsync(invitation);
    }
}