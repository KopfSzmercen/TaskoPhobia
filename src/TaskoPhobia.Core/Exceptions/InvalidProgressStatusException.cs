using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidProgressStatusException : CustomException
{
    public InvalidProgressStatusException() : base("Invalid progress status.")
    {
    }
}