using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Invitations.Rules;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class InvitationTests : TestBase
{
    [Fact]
    public void reject_invitation_should_throw_OnlyPendingInvitationStatusCanBeChangedRule_when_already_accepted()
    {
        var invitation = CreateInvitation();
        invitation.Reject(false);
        AssertBrokenRule<OnlyPendingInvitationStatusCanBeChangedRule>(() => invitation.Reject(false));
    }

    [Fact]
    public void Accept_Accepted_Invitation_Should_Throw_OnlyPendingInvitationStatusCanBeChangedRule_Exception()
    {
        var invitation = CreateInvitation();
        invitation.Accept();

        AssertBrokenRule<OnlyPendingInvitationStatusCanBeChangedRule>(() => invitation.Accept());

        AssertBrokenRule<OnlyPendingInvitationStatusCanBeChangedRule>(() => invitation.Accept());
    }

    [Fact]
    public void Accept_Rejected_Invitation_Should_Throw_OnlyPendingInvitationStatusCanBeChangedRule_Exception()
    {
        var invitation = CreateInvitation();
        invitation.Reject(true);
        AssertBrokenRule<OnlyPendingInvitationStatusCanBeChangedRule>(() => invitation.Accept());
    }

    #region setup

    private static readonly IClock Clock = new Clock();

    private static Invitation CreateInvitation()
    {
        var invitation = Invitation.CreateNew(Guid.NewGuid(), Guid.NewGuid(), "Title", Guid.NewGuid(), Guid.NewGuid(),
            Clock);
        return invitation;
    }

    #endregion
}