using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class NotAllowedToCreateMoreProjectsException : CustomException
{
    public NotAllowedToCreateMoreProjectsException() : base(
        "You are not allowed to create more projects with your account type.")
    {
    }
}