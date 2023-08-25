#nullable enable

using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;

namespace TaskoPhobia.Core.Entities.Products;

public class Order
{
    private Order(OrderId id, ProductId productId, Money price, DateTimeOffset createdAt, UserId customerId,
        OrderStatus status)
    {
        Id = id;
        ProductId = productId;
        Price = price;
        CreatedAt = createdAt;
        CustomerId = customerId;
        Status = status;
    }

    protected Order()
    {
    }

    public OrderId Id { get; }
    public ProductId ProductId { get; }
    public Money Price { get; }
    public DateTimeOffset CreatedAt { get; }
    public UserId CustomerId { get; }
    public OrderStatus Status { get; }

    public static Order NewFromProduct(OrderId orderId, Product product, UserId customerId, DateTimeOffset createdAt)
    {
        return new Order(orderId, product.Id, Money.Create(product.Price.Amount, product.Price.Currency), createdAt,
            customerId, OrderStatus.New());
    }
}