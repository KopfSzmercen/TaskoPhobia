using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Invitations.Rules;

public class CanCreateIfInvitationIsNotAlreadySentToUserRule : IBusinessRule
{
    private readonly IEnumerable<Invitation> _invitations;
    private readonly UserId _receiverId;

    public CanCreateIfInvitationIsNotAlreadySentToUserRule(IEnumerable<Invitation> invitations, UserId receiverId)
    {
        _invitations = invitations;
        _receiverId = receiverId;
    }

    public string Message => "Invitation is already sent to user.";

    public bool IsBroken()
    {
        return _invitations.Any(
            i => i.ReceiverId == _receiverId && i.Status == InvitationStatus.Pending());
    }
}