using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.DomainServices.Invitations;

public interface IInvitationService
{
    void AcceptInvitationAndJoinProject(Invitation invitation, UserId receiverId);

    Task<Invitation> CreateInvitation(InvitationId invitationId, Project project,
        UserId senderId, UserId receiverId);
}