using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Projects.Rules;

internal sealed class BlockedSendingMoreInvitationsRule : IBusinessRule
{
    private readonly Invitation _invitation;
    private readonly Project _project;

    public BlockedSendingMoreInvitationsRule(Project project, Invitation invitation)
    {
        _project = project;
        _invitation = invitation;
    }

    public string Message => "Can not send invitation. User blocked sending more invitations";

    public bool IsBroken()
    {
        return _project.Invitations.Any(i => i.BlockSendingMoreInvitations && i.ReceiverId == _invitation.ReceiverId);
    }
}