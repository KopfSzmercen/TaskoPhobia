using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public sealed class UserNotFoundException : CustomException
{
    public UserNotFoundException(UserId userId) : base($"User {userId} not found")
    {
    }
}