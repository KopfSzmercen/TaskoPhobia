using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseName;

public class InvalidNameException : CustomException
{
    public InvalidNameException() : base("Invalid name")
    {
    }
}