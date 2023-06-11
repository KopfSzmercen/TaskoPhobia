using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidProjectNameException : CustomException
{
    public InvalidProjectNameException(string projectName) : base($"{projectName} is not a valid project name.")
    {
    }
}