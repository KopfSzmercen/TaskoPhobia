using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidTaskTimeSpanException : CustomException
{
    public InvalidTaskTimeSpanException() : base("Given time span is invalid.")
    {
    }
}