using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Entities.Products.ValueObjects.Exceptions;

public sealed class InvalidOrderStatusException : CustomException
{
    public InvalidOrderStatusException() : base("Invalid order status.")
    {
    }
}