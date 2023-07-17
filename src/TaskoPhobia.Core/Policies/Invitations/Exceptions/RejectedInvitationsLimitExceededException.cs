using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Policies.Invitations.Exceptions;

public class RejectedInvitationsLimitExceededException : CustomException
{
    public RejectedInvitationsLimitExceededException() : base(
        "Can't invite user to project. Number of rejected invitations exceeded.")
    {
    }
}