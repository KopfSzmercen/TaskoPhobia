using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.Policies;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.DomainServices;

public sealed class InvitationService : IInvitationService
{
    private readonly IClock _clock;
    private readonly IEnumerable<IInvitationPolicy> _policies;

    public InvitationService(IEnumerable<IInvitationPolicy> policies, IClock clock)
    {
        _policies = policies;
        _clock = clock;
    }

    public void CreateInvitationToProject(Project project, UserId senderId, Invitation invitation)
    {
        var canCreate = _policies.Any(x => x.CanCreate(project, senderId));

        if (!canCreate) throw new NotAllowedToCreateInvitation();

        project.AddInvitation(invitation);
    }

    public void AcceptInvitationAndJoinProject(Invitation invitation, UserId receiverId)
    {
        if (invitation.ReceiverId != receiverId) throw new InvitationCanNotBeAcceptedException();

        invitation.Accept();

        var projectParticipation = ProjectParticipation.CreateNew(invitation.ProjectId, invitation.ReceiverId, _clock);
        invitation.Project.AddParticipation(projectParticipation);
    }
}