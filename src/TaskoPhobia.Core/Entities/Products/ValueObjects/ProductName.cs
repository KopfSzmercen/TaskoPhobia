using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseName;

namespace TaskoPhobia.Core.Entities.Products.ValueObjects;

public class ProductName : BaseName
{
    public ProductName(string value) : base(value)
    {
    }

    public static implicit operator string(ProductName value)
    {
        return value.Value;
    }

    public static implicit operator ProductName(string value)
    {
        return new ProductName(value);
    }
}