using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Products.Events;

public class OrderCreatedDomainEvent : DomainEventBase
{
    public OrderCreatedDomainEvent(Guid orderId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
}