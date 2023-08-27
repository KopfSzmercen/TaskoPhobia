using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseId;

namespace TaskoPhobia.Core.Entities.Products.ValueObjects;

public class OrderId : BaseId
{
    public OrderId(Guid value) : base(value)
    {
    }

    public static implicit operator Guid(OrderId value)
    {
        return value.Value;
    }

    public static implicit operator OrderId(Guid value)
    {
        return new OrderId(value);
    }
}