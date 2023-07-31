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