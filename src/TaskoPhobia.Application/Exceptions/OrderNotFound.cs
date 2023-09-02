using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public sealed class OrderNotFound : CustomException
{
    public OrderNotFound() : base("Order not found.")
    {
    }
}