using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Policies.Invitations.Exceptions;

namespace TaskoPhobia.Core.Policies.Invitations;

internal sealed class UserAlreadyParticipatesProjectPolicy : ICreateInvitationPolicy
{
    public void Validate(Project project, Invitation invitation)
    {
        if (project.Participations.Any(p => p.ParticipantId == invitation.ReceiverId))
            throw new UserAlreadyParticipatesProjectException();
    }
}