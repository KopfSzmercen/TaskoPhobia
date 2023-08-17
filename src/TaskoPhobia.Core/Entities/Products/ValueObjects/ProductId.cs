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