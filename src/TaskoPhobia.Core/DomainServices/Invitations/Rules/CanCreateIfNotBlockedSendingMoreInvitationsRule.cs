using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Invitations.Rules;

internal sealed class CanCreateIfNotBlockedSendingMoreInvitationsRule : IBusinessRule
{
    private readonly IEnumerable<Invitation> _invitations;
    private readonly UserId _receiverId;

    public CanCreateIfNotBlockedSendingMoreInvitationsRule(UserId receiverId,
        IEnumerable<Invitation> invitations)
    {
        _receiverId = receiverId;
        _invitations = invitations;
    }

    public string Message => "Can not send invitation. User blocked sending more invitations";

    public bool IsBroken()
    {
        return _invitations.Any(i => i.BlockSendingMoreInvitations && i.ReceiverId == _receiverId);
    }
}