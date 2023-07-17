using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Policies.Invitations.Exceptions;

namespace TaskoPhobia.Core.Policies.Invitations;

internal sealed class ProjectOwnerPolicy : ICreateInvitationPolicy
{
    public void Validate(Project project, Invitation invitation)
    {
        if (project.OwnerId != invitation.SenderId) throw new NotAllowedToCreateInvitation();
    }
}