using TaskoPhobia.Core.Entities.Products.ValueObjects.Exceptions;

namespace TaskoPhobia.Core.Entities.Products.ValueObjects;

public class ProductDescription
{
    public ProductDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3)
            throw new InvalidProductDescription(value);

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(ProductDescription value)
    {
        return value.Value;
    }

    public static implicit operator ProductDescription(string value)
    {
        return new ProductDescription(value);
    }
}