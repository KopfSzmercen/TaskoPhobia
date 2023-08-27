using TaskoPhobia.Core.Entities.Products.ValueObjects.Exceptions;

namespace TaskoPhobia.Core.Entities.Products.ValueObjects;

public class OrderStatus
{
    private readonly HashSet<string> _availableStatuses = new() { "NEW", "COMPLETED" };

    public OrderStatus(string value)
    {
        if (!_availableStatuses.Any(x => string.Equals(x, value))) throw new InvalidOrderStatusException();
        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(OrderStatus value)
    {
        return value.Value;
    }

    public static implicit operator OrderStatus(string value)
    {
        return new OrderStatus(value);
    }

    public static OrderStatus New()
    {
        return "NEW";
    }

    public static OrderStatus Completed()
    {
        return "COMPLETED";
    }
}