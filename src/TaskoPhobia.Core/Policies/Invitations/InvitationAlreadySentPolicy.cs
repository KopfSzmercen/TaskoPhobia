using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Policies.Invitations.Exceptions;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies.Invitations;

internal sealed class InvitationAlreadySentPolicy : ICreateInvitationPolicy
{
    public void Validate(Project project, Invitation invitation)
    {
        if (project.Invitations.Any(
                i => i.ReceiverId == invitation.ReceiverId && i.Status == InvitationStatus.Pending()))
            throw new InvitationAlreadySentException(invitation.ReceiverId);
    }
}