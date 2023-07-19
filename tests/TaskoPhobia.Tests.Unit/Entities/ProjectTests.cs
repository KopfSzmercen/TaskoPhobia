using Shouldly;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Projects.Rules;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class ProjectTests : TestBase
{
    [Fact]
    public void add_task_when_project_is_finished_should_throw_NotAllowedToModifyFinishedProject_exception()
    {
        var project = CreateProjectWithGivenStatus(ProgressStatus.Finished());
        AssertBrokenRule<FinishedProjectCanNotBeModifiedRule>(() => project.AddTask(CreateTaskForProject(project.Id)));
    }

    [Fact]
    public void add_task_when_project_is_in_progress_should_work()
    {
        var project = CreateProjectWithGivenStatus(ProgressStatus.InProgress());
        project.AddTask(CreateTaskForProject(project.Id));
        project.Tasks.Count().ShouldBe(1);
    }

    [Fact]
    public void add_invitation_when_project_is_finished_should_throw_NotAllowedToModifyFinishedProject_exception()
    {
        var project = CreateProjectWithGivenStatus(ProgressStatus.Finished());
        var invitation = CreateInvitation(project.OwnerId, Guid.NewGuid());

        AssertBrokenRule<FinishedProjectCanNotBeModifiedRule>(() => project.AddInvitation(invitation));
    }

    [Fact]
    public void
        add_invitation_when_receiver_is_already_participating_should_throw_ReceiverMustNotParticipateProjectRule()
    {
        var project = CreateProjectWithGivenStatus(ProgressStatus.InProgress());
        var invitation = CreateInvitation(project.OwnerId, Guid.NewGuid());

        AddReceiverAsProjectParticipant(project, invitation.ReceiverId);
        AssertBrokenRule<ReceiverMustNotParticipateProjectRule>(() => project.AddInvitation(invitation));
    }

    [Fact]
    public void add_invitation_when_sender_is_not_project_owner_should_throw_InvitationSenderMustBeProjectOwnerRule()
    {
        var project = CreateProjectWithGivenStatus(ProgressStatus.InProgress());
        var invitation = CreateInvitation(Guid.NewGuid(), Guid.NewGuid());

        AssertBrokenRule<InvitationSenderMustBeProjectOwnerRule>(() => project.AddInvitation(invitation));
    }

    [Fact]
    public void add_invitation_when_invitation_already_sent_to_user_should_throw_InvitationIsAlreadySentToUserRule()
    {
        var receiverId = Guid.NewGuid();
        var project = CreateProjectWithGivenStatus(ProgressStatus.InProgress());
        var firstInvitationForReceiver = CreateInvitation(project.OwnerId, receiverId);
        project.AddInvitation(firstInvitationForReceiver);

        var secondInvitationForReceiver = CreateInvitation(project.OwnerId, receiverId);

        AssertBrokenRule<InvitationIsAlreadySentToUserRule>(() =>
            project.AddInvitation(secondInvitationForReceiver));
    }

    [Fact]
    public void
        add_invitation_when_rejected_invitations_limit_exceeded_should_throw_RejectedInvitationsLimitIsNotExceededRule()
    {
        var receiverId = Guid.NewGuid();
        var project = CreateProjectWithGivenStatus(ProgressStatus.InProgress());

        for (var i = 0; i < RejectedInvitationsLimitIsNotExceededRule.RejectedInvitationsLimit; i++)
        {
            var invitation = CreateInvitation(project.OwnerId, receiverId);
            project.AddInvitation(invitation);
            invitation.Reject(false);
        }

        AssertBrokenRule<RejectedInvitationsLimitIsNotExceededRule>(() =>
            project.AddInvitation(CreateInvitation(project.OwnerId, receiverId)));
    }

    #region setup

    private static readonly IClock Clock = new Clock();

    private static Project CreateProjectWithGivenStatus(ProgressStatus progressStatus)
    {
        var project = new Project(Guid.NewGuid(), "Project", "description", progressStatus, DateTime.Now,
            Guid.NewGuid());
        return project;
    }

    private static ProjectTask CreateTaskForProject(ProjectId projectId)
    {
        var task = new ProjectTask(Guid.NewGuid(), "Project",
            new TaskTimeSpan(DateTime.UtcNow, DateTime.UtcNow.AddDays(5)), projectId,
            ProgressStatus.InProgress());

        return task;
    }

    private static Invitation CreateInvitation(Guid senderId, Guid receiverId)
    {
        var invitation = Invitation.CreateNew(Guid.NewGuid(), "Title", senderId, receiverId, Clock);
        return invitation;
    }

    private static void AddReceiverAsProjectParticipant(Project project, Guid participantId)
    {
        var projectParticipation = ProjectParticipation.CreateNew(project.Id, participantId, Clock);
        project.AddParticipation(projectParticipation);
    }

    #endregion
}