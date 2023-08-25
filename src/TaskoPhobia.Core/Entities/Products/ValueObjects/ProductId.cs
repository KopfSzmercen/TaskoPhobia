using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseId;

namespace TaskoPhobia.Core.Entities.Products.ValueObjects;

public class ProductId : BaseId
{
    public ProductId(Guid value) : base(value)
    {
    }

    public static implicit operator ProductId(Guid value)
    {
        return new ProductId(value);
    }

    public static implicit operator Guid(ProductId value)
    {
        return value.Value;
    }
}

/*public sealed record ProductId
{
    public ProductId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);

        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator ProductId(string value)
    {
        return Guid.Parse(value);
    }

    public static implicit operator Guid(ProductId value)
    {
        return value.Value;
    }

    public static implicit operator ProductId(Guid value)
    {
        return new ProductId(value);
    }
} */