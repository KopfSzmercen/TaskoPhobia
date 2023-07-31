using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Services;

public interface IInvitationReadService
{
    public Task<IEnumerable<Invitation>> GetInvitationsToProjectForReceiver(ProjectId projectId,
        UserId receiverId);
}