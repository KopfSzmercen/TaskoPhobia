using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Invitations.Rules;

public class OnlyPendingInvitationStatusCanBeChangedRule : IBusinessRule
{
    private readonly Invitation _invitation;

    public OnlyPendingInvitationStatusCanBeChangedRule(Invitation invitation)
    {
        _invitation = invitation;
    }

    public string Message => "Status of invitation may only be changed if the current status is pending.";

    public bool IsBroken()
    {
        return _invitation.Status != InvitationStatus.Pending();
    }
}