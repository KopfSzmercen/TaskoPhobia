using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseId.Exceptions;

public class InvalidBaseIdException : CustomException
{
    public InvalidBaseIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
    {
    }
}