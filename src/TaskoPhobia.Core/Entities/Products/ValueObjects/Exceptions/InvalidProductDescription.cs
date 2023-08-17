using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Entities.Products.ValueObjects.Exceptions;

public class InvalidProductDescription : CustomException
{
    public InvalidProductDescription(string value) : base($"{value} is not a valid product description")
    {
    }
}