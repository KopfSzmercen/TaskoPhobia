using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Policies.Invitations.Exceptions;

namespace TaskoPhobia.Core.Policies.Invitations;

internal sealed class BlockedSendingMoreInvitationsPolicy : ICreateInvitationPolicy
{
    public void Validate(Project project, Invitation invitation)
    {
        if (project.Invitations.Any(i => i.BlockSendingMoreInvitations && i.ReceiverId == invitation.ReceiverId))
            throw new CanNotSendMoreInvitationsException();
    }
}