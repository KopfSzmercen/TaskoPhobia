using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvalidRoleException : CustomException
{
    public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
    {
        Role = role;
    }

    public string Role { get; }
}