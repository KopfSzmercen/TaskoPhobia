using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Repositories;

public interface IInvitationRepository
{
    Task<Invitation> FindByIdAsync(InvitationId id);
    Task UpdateAsync(Invitation invitation);
}