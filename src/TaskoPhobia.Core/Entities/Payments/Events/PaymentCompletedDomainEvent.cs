using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Events;

public class PaymentCompletedDomainEvent : DomainEventBase
{
    public PaymentCompletedDomainEvent(Guid paymentId, Guid orderId)
    {
        PaymentId = paymentId;
        OrderId = orderId;
    }

    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
}