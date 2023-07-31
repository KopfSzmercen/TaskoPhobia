using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Invitations.Rules;

public class CanCreateIfRejectedInvitationsLimitIsNotExceededRule : IBusinessRule
{
    public const ushort RejectedInvitationsLimit = 3;
    private readonly IEnumerable<Invitation> _invitations;

    public CanCreateIfRejectedInvitationsLimitIsNotExceededRule(IEnumerable<Invitation> invitations)
    {
        _invitations = invitations;
    }

    public string Message => "Can't invite user to project. Number of rejected invitations exceeded.";

    public bool IsBroken()
    {
        var numOfRejectedInvitations = _invitations.Count(x => x.Status.Equals(InvitationStatus.Rejected()));
        return numOfRejectedInvitations >= RejectedInvitationsLimit;
    }
}