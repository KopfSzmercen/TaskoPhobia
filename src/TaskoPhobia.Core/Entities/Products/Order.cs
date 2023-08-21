#nullable enable

using TaskoPhobia.Core.Entities.Products.Events;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;

namespace TaskoPhobia.Core.Entities.Products;

public class Order : Entity
{
    protected Order(OrderId id, ProductId productId, Money price, DateTimeOffset createdAt, UserId customerId,
        OrderStatus status)
    {
        Id = id;
        ProductId = productId;
        Price = price;
        CreatedAt = createdAt;
        CustomerId = customerId;
        Status = status;

        AddDomainEvent(new OrderCreatedDomainEvent(Id));
    }

    public Order()
    {
    }

    public OrderId Id { get; }
    public ProductId ProductId { get; }
    public Money Price { get; init; }
    public DateTimeOffset CreatedAt { get; }

    //TODO - Add payments later
    //public Payment Payment { get; init; }
    public UserId CustomerId { get; init; }
    public OrderStatus Status { get; }

    public static Order NewFromProduct(OrderId orderId, Product product, UserId customerId,
        DateTimeOffset createdAt)
    {
        return new Order(orderId, product.Id, product.Price, createdAt, customerId, OrderStatus.New());
    }
}