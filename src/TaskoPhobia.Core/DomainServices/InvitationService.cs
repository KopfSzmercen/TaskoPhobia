using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.DomainServices;

public sealed class InvitationService : IInvitationService
{
    private readonly IClock _clock;

    public InvitationService(IClock clock)
    {
        _clock = clock;
    }


    public void AcceptInvitationAndJoinProject(Invitation invitation, UserId receiverId)
    {
        if (invitation.ReceiverId != receiverId) throw new InvitationCanNotBeAcceptedException();

        invitation.Accept();

        var projectParticipation = ProjectParticipation.CreateNew(invitation.ProjectId, invitation.ReceiverId, _clock);
        invitation.Project.AddParticipation(projectParticipation);
    }
}