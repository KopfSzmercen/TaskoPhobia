using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies;

internal sealed class ProjectOwnerPolicy : IInvitationPolicy
{
    public bool CanCreate(Project project, UserId userId)
    {
        return project.OwnerId == userId;
    }
}