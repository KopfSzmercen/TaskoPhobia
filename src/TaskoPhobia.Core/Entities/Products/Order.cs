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
    public OrderStatus Status { get; private set; }

    public static Order New(OrderId orderId, ProductId productId, Money moneyToPay, UserId customerId,
        DateTimeOffset createdAt)
    {
        return new Order(orderId, productId, moneyToPay, createdAt,
            customerId, OrderStatus.New());
    }

    public void Complete()
    {
        Status = OrderStatus.Completed();
    }

    public bool IsCompleted()
    {
        return Status.Value.Equals(OrderStatus.Completed());
    }
}