using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidProjectDescriptionException : CustomException
{
    public InvalidProjectDescriptionException() : base("Provided project description is not valid")
    {
    }
}