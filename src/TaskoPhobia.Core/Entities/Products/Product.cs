using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;

namespace TaskoPhobia.Core.Entities.Products;

public class Product
{
    protected Product(ProductId id, ProductName name, Money price, ProductDescription description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public Product()
    {
    }

    public ProductId Id { get; }
    public ProductName Name { get; }
    public Money Price { get; }
    public ProductDescription Description { get; }
}