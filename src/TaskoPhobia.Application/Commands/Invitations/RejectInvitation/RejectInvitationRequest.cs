namespace TaskoPhobia.Application.Commands.Invitations.RejectInvitation;

public class RejectInvitationRequest
{
    public bool BlockSendingMoreInvitations { get; init; }

    public RejectInvitation ToCommand(Guid invitationId, Guid receiverId)
    {
        return new RejectInvitation(invitationId, receiverId, BlockSendingMoreInvitations);
    }
}