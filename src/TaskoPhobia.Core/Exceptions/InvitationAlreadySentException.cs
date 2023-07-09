using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvitationAlreadySentException : CustomException
{
    public InvitationAlreadySentException(Guid receiverId) : base(
        $"Invitation for user {receiverId} is already sent.")
    {
    }
}