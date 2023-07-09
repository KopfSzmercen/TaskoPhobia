using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidInvitationTitleException : CustomException
{
    public InvalidInvitationTitleException() : base("Invalid invitation name!")
    {
    }
}