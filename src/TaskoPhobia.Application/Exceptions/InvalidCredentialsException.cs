using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public sealed class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}