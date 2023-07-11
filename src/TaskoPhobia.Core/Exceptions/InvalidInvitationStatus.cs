using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidInvitationStatus : CustomException
{
    public InvalidInvitationStatus() : base("Invalid invitation status.")
    {
    }
}