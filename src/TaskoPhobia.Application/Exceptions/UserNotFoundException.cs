using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public sealed class UserNotFoundException : CustomException 
{
    public UserNotFoundException() : base("User not found")
    {
    }
}