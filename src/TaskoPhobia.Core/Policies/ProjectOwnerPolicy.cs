using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies;

internal sealed class ProjectOwnerPolicy : IInvitationPolicy
{
    public bool CanBeApplied()
    {
        return true;
    }

    public bool CanCreate(Project project, UserId userId)
    {
        return project.OwnerId == userId;
    }
}