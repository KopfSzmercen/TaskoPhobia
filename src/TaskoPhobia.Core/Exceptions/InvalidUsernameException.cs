namespace TaskoPhobia.Core.Exceptions;

public class InvalidUsernameException : CustomException
{
    public InvalidUsernameException(string value) : base($"{value} is not a valid username.")
    {
    }
}