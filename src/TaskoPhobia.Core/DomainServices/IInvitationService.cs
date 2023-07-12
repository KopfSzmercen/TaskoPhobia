using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.DomainServices;

public interface IInvitationService
{
    void CreateInvitationToProject(Project project, UserId senderId, Invitation invitation);
    void AcceptInvitationAndJoinProject(Invitation invitation);
}