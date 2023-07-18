using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Rules;

public class InvitationIsAlreadySentToUserRule : IBusinessRule
{
    private readonly Invitation _invitation;
    private readonly Project _project;

    public InvitationIsAlreadySentToUserRule(Project project, Invitation invitation)
    {
        _project = project;
        _invitation = invitation;
    }

    public string Message => "Invitation is already sent to user.";

    public bool IsBroken()
    {
        return _project.Invitations.Any(
            i => i.ReceiverId == _invitation.ReceiverId && i.Status == InvitationStatus.Pending());
    }
}