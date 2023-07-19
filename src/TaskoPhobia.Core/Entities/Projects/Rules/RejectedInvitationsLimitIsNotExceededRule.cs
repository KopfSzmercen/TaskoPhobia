using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Projects.Rules;

public class RejectedInvitationsLimitIsNotExceededRule : IBusinessRule
{
    private const ushort RejectedInvitationsLimit = 3;
    private readonly Invitation _invitation;
    private readonly Project _project;

    public RejectedInvitationsLimitIsNotExceededRule(Project project, Invitation invitation)
    {
        _invitation = invitation;
        _project = project;
    }

    public string Message => "Can't invite user to project. Number of rejected invitations exceeded.";

    public bool IsBroken()
    {
        var numOfRejectedInvitations =
            _project.Invitations.Count(
                i => i.ReceiverId == _invitation.ReceiverId && i.Status == InvitationStatus.Rejected());

        return numOfRejectedInvitations >= RejectedInvitationsLimit;
    }
}