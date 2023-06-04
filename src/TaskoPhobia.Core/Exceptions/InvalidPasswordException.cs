namespace TaskoPhobia.Core.Exceptions;

public class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}