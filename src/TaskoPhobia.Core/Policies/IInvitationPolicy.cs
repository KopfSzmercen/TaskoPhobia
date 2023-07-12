using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies;

public interface IInvitationPolicy
{
    bool CanCreate(Project project, UserId userId);
}