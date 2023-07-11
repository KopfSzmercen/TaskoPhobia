using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.DomainServices;

public interface IInvitationService
{
    void CreateInvitationToProject(Project project, UserId senderId, UserId receiverId, Invitation invitation);
}