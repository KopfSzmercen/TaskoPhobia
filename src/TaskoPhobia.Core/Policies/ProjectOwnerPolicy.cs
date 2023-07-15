using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Policies;

internal sealed class ProjectOwnerPolicy : ICreateInvitationPolicy
{
    public bool CanCreate(Project project, UserId senderId)
    {
        return project.OwnerId == senderId;
    }
}