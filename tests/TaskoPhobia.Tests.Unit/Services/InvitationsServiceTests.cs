using NSubstitute;
using Shouldly;
using TaskoPhobia.Core.Common.Rules;
using TaskoPhobia.Core.DomainServices.Invitations;
using TaskoPhobia.Core.DomainServices.Invitations.Rules;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Services;

public class InvitationsServiceTests : TestBase
{
    private readonly IInvitationReadService _invitationReadService = Substitute.For<IInvitationReadService>();

    private readonly IProjectParticipationReadService _projectParticipationReadService =
        Substitute.For<IProjectParticipationReadService>();

    private readonly InvitationService _sut;

    public InvitationsServiceTests()
    {
        _sut = new InvitationService(new Clock(), _invitationReadService, _projectParticipationReadService);
    }


    [Fact]
    public void AcceptInvitationAndCreateProjectParticipation_ShouldFail_IfReceiverIdNotEqualsUserId()
    {
        var invitation = Invitation.CreateNew(Guid.NewGuid(), Guid.NewGuid(),
            "invitation", Guid.NewGuid(), Guid.NewGuid(), new Clock());

        var userId = Guid.NewGuid();

        Should.Throw<InvitationCanNotBeAcceptedException>(() => _sut.AcceptInvitationAndCreateProjectParticipation(
            invitation, userId
        ));
    }

    [Fact]
    public void
        AcceptInvitationAndCreateProjectParticipation_ShouldCreateProjectParticipation_IfReceiverIdEqualsUserId()
    {
        var receiverId = Guid.NewGuid();
        var invitation = Invitation.CreateNew(Guid.NewGuid(), Guid.NewGuid(),
            "invitation", Guid.NewGuid(), receiverId, new Clock());

        var result = _sut.AcceptInvitationAndCreateProjectParticipation(invitation, receiverId);

        result.ShouldBeOfType<ProjectParticipation>();
    }

    [Fact]
    public async void CreateInvitation_ShouldThrow_FinishedProjectCanNotBeModifiedRuleException_IfProjectIsFinished()
    {
        var projectOwnerId = Guid.NewGuid();

        var project = Project.CreateNew(Guid.NewGuid(), "name", "desc", new Clock(), projectOwnerId);
        project.Finish(projectOwnerId, true);

        await AssertBrokenRuleAsync<FinishedProjectCanNotBeModifiedRule>(() =>
            _sut.CreateInvitation(Guid.NewGuid(), project, projectOwnerId, Guid.NewGuid()));
    }

    [Fact]
    public async void
        CreateInvitation_ShouldThrow_CanCreateIfSenderIsProjectOwnerRuleException_IfSenderIsNotProjectOwner()
    {
        var projectOwnerId = Guid.NewGuid();
        var project = Project.CreateNew(Guid.NewGuid(), "name", "desc", new Clock(), projectOwnerId);

        await AssertBrokenRuleAsync<CanCreateIfSenderIsProjectOwnerRule>(() =>
            _sut.CreateInvitation(Guid.NewGuid(), project, Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async void
        CreateInvitation_ShouldThrow_CanCreateIfReceiverNotParticipateProjectRule_IfReceiverAlreadyParticipateProject()
    {
        var projectOwnerId = Guid.NewGuid();
        var receiverId = Guid.NewGuid();

        var project = Project.CreateNew(Guid.NewGuid(), "name", "desc", new Clock(), projectOwnerId);

        _projectParticipationReadService.IsUserProjectParticipantAsync(project.Id, receiverId).Returns(true);

        await AssertBrokenRuleAsync<CanCreateIfReceiverNotParticipateProjectRule>(() =>
            _sut.CreateInvitation(Guid.NewGuid(), project, projectOwnerId, receiverId));
    }

    [Fact]
    public async void
        CreateInvitation_ShouldThrow_CanCreateIfReceiverNotParticipateProjectRule_IfReceiverAlreadyHasPendingInvitation()
    {
        var projectOwnerId = Guid.NewGuid();
        var receiverId = Guid.NewGuid();

        var project = Project.CreateNew(Guid.NewGuid(), "name", "desc", new Clock(), projectOwnerId);

        var expectedInvitation =
            Invitation.CreateNew(Guid.NewGuid(), project.Id, "title", projectOwnerId, receiverId, new Clock());

        _projectParticipationReadService.IsUserProjectParticipantAsync(project.Id, receiverId).Returns(false);

        _invitationReadService.GetInvitationsToProjectForReceiver(project.Id, receiverId)
            .Returns(new List<Invitation> { expectedInvitation });

        await AssertBrokenRuleAsync<CanCreateIfInvitationIsNotAlreadySentToUserRule>(() =>
            _sut.CreateInvitation(Guid.NewGuid(), project, projectOwnerId, receiverId));
    }

    [Fact]
    public async void CreateInvitation_ShouldReturnInvitation_IfCreateDataIsValid()
    {
        var projectOwnerId = Guid.NewGuid();
        var receiverId = Guid.NewGuid();

        var project = Project.CreateNew(Guid.NewGuid(), "name", "desc", new Clock(), projectOwnerId);

        _projectParticipationReadService.IsUserProjectParticipantAsync(project.Id, receiverId).Returns(false);
        _invitationReadService.GetInvitationsToProjectForReceiver(project.Id, receiverId)
            .Returns(new List<Invitation>());

        var result = await _sut.CreateInvitation(Guid.NewGuid(), project, projectOwnerId, receiverId);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<Invitation>();
    }
}