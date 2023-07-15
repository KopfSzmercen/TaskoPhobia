using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies;

internal sealed class RejectedInvitationsLimitPolicy
{
    private const ushort RejectedInvitationsLimit = 3;
    private readonly IEnumerable<Invitation> _invitations;
    private readonly Invitation _newInvitation;

    public RejectedInvitationsLimitPolicy(IEnumerable<Invitation> invitations, Invitation newInvitation)
    {
        _invitations = invitations;
        _newInvitation = newInvitation;
    }

    public void Validate()
    {
        var numOfRejectedInvitations =
            _invitations.Count(
                i => i.ReceiverId == _newInvitation.ReceiverId && i.Status == InvitationStatus.Rejected());

        if (numOfRejectedInvitations >= RejectedInvitationsLimit) throw new RejectedInvitationsLimitExceededException();
    }
}