using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.Invitations.RejectInvitation;

public class RejectInvitationHandler : ICommandHandler<RejectInvitation>
{
    private readonly IContext _context;
    private readonly IInvitationRepository _invitationRepository;

    public RejectInvitationHandler(IInvitationRepository invitationRepository, IContext context)
    {
        _invitationRepository = invitationRepository;
        _context = context;
    }

    public async Task HandleAsync(RejectInvitation command)
    {
        var invitation = await _invitationRepository.FindByIdAsync(command.InvitationId);
        if (invitation is null) throw new InvitationNotFoundException(command.InvitationId);

        if (invitation.ReceiverId != _context.Identity.Id) throw new NotAllowedToRejectInvitationException();

        invitation.Reject(command.BlockSendingMoreInvitations);

        await _invitationRepository.UpdateAsync(invitation);
    }
}