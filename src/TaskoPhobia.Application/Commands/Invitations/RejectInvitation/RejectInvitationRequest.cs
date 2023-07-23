namespace TaskoPhobia.Application.Commands.Invitations.RejectInvitation;

public class RejectInvitationRequest
{
    public bool BlockSendingMoreInvitations { get; init; }

    public RejectInvitation ToCommand(Guid invitationId)
    {
        return new RejectInvitation(invitationId, BlockSendingMoreInvitations);
    }
}