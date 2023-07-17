using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Policies.Invitations.Exceptions;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies.Invitations;

internal sealed class RejectedInvitationsLimitPolicy : ICreateInvitationPolicy
{
    private const ushort RejectedInvitationsLimit = 3;

    public void Validate(Project project, Invitation invitation)
    {
        var numOfRejectedInvitations =
            project.Invitations.Count(
                i => i.ReceiverId == invitation.ReceiverId && i.Status == InvitationStatus.Rejected());

        if (numOfRejectedInvitations >= RejectedInvitationsLimit) throw new RejectedInvitationsLimitExceededException();
    }
}