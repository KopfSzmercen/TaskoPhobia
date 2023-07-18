using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public class NotAllowedToRejectInvitationException : CustomException
{
    public NotAllowedToRejectInvitationException() : base("Not allowed to reject invitation.")
    {
    }
}