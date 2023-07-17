using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.DomainServices;

public interface IInvitationService
{
    void AcceptInvitationAndJoinProject(Invitation invitation, UserId receiverId);
}