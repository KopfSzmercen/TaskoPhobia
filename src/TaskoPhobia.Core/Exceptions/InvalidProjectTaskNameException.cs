using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidProjectTaskNameException : CustomException
{
    public InvalidProjectTaskNameException(string value) : base($"{value} is not a valid project task name.")
    {
    }
}