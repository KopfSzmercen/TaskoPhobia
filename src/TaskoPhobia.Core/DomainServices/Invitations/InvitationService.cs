using TaskoPhobia.Core.Common.Rules;
using TaskoPhobia.Core.DomainServices.Invitations.Rules;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.DomainServices.Invitations;

internal sealed class InvitationService : DomainService, IInvitationService
{
    private readonly IClock _clock;
    private readonly IInvitationReadService _invitationReadService;
    private readonly IProjectParticipationReadService _projectParticipationReadService;

    public InvitationService(IClock clock, IInvitationReadService invitationReadService,
        IProjectParticipationReadService projectParticipationReadService)
    {
        _clock = clock;
        _invitationReadService = invitationReadService;
        _projectParticipationReadService = projectParticipationReadService;
    }

    public ProjectParticipation AcceptInvitationAndCreateProjectParticipation(Invitation invitation, UserId receiverId)
    {
        if (invitation.ReceiverId != receiverId) throw new InvitationCanNotBeAcceptedException();

        invitation.Accept();

        return ProjectParticipation.CreateNew(invitation.ProjectId, invitation.ReceiverId, _clock);
    }

    public async Task<Invitation> CreateInvitation(InvitationId invitationId, Project project, UserId senderId,
        UserId receiverId)
    {
        CheckRule(new FinishedProjectCanNotBeModifiedRule(project));
        CheckRule(new CanCreateIfSenderIsProjectOwnerRule(project, senderId));

        var receiverParticipatesProject =
            await _projectParticipationReadService.IsUserProjectParticipantAsync(project.Id, receiverId);

        CheckRule(new CanCreateIfReceiverNotParticipateProjectRule(receiverParticipatesProject));

        var invitationsToProjectForReceiver =
            (await _invitationReadService.GetInvitationsToProjectForReceiver(project.Id, receiverId)).ToArray();

        CheckRule(new CanCreateIfNotBlockedSendingMoreInvitationsRule(receiverId, invitationsToProjectForReceiver));
        CheckRule(new CanCreateIfInvitationIsNotAlreadySentToUserRule(invitationsToProjectForReceiver, receiverId));
        CheckRule(new CanCreateIfRejectedInvitationsLimitIsNotExceededRule(invitationsToProjectForReceiver));

        return Invitation.CreateNew(invitationId, project.Id, $"Invitation for project: {project.Name}", senderId,
            receiverId,
            _clock);
    }
}