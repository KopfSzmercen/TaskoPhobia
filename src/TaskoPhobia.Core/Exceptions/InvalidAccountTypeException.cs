using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidAccountTypeException : CustomException
{
    public InvalidAccountTypeException(string value) : base($"{value} is not a valid account type.")
    {
    }
}