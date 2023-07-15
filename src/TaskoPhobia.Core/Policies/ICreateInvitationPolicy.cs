using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies;

public interface ICreateInvitationPolicy
{
    bool CanCreate(Project project, UserId senderId);
}