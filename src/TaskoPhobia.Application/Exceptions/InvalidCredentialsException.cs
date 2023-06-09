using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public sealed class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}