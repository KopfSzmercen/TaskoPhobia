using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.Policies;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.DomainServices;

public sealed class InvitationService : IInvitationService
{
    private readonly IEnumerable<IInvitationPolicy> _policies;

    public InvitationService(IEnumerable<IInvitationPolicy> policies)
    {
        _policies = policies;
    }

    public void CreateInvitationToProject(Project project, UserId senderId, Invitation invitation)
    {
        var policy = _policies.FirstOrDefault(x => x.CanBeApplied());

        if (policy is null) throw new NoInvitationPolicyFoundException();

        if (!policy.CanCreate(project, senderId)) throw new NotAllowedToCreateInvitation();

        project.AddInvitation(invitation);
    }

    public void AcceptInvitationAndJoinProject(Invitation invitation)
    {
        invitation.Accept();

        var projectParticipation = ProjectParticipation.CreateNew(invitation.ProjectId, invitation.ReceiverId);
        invitation.Project.AddParticipant(projectParticipation);
    }
}