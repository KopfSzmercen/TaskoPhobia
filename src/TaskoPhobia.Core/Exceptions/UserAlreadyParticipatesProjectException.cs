using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class UserAlreadyParticipatesProjectException : CustomException
{
    public UserAlreadyParticipatesProjectException() : base(
        "Can not invite user. User already participates the project.")
    {
    }
}