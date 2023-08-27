using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

public sealed class InvalidUrlException : CustomException
{
    public InvalidUrlException(string value) : base($"{value} is not a valid redirect url")
    {
    }
}