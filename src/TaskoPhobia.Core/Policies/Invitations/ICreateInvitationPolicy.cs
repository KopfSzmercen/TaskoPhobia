using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Core.Policies.Invitations;

public interface ICreateInvitationPolicy
{
    void Validate(Project project, Invitation invitation);
}