using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class RejectedInvitationsLimitExceededException : CustomException
{
    public RejectedInvitationsLimitExceededException() : base(
        "Can't invite user to project. Number of rejected invitations exceeded.")
    {
    }
}