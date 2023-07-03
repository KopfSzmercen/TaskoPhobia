using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public sealed class EmailExistsException : CustomException
{
    public EmailExistsException() : base("Email already in use.")
    {
    }
}