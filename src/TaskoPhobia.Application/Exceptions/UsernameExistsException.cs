using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public sealed class UsernameExistsException : CustomException
{
    public UsernameExistsException() : base("Username already in use.")
    {
    }
}