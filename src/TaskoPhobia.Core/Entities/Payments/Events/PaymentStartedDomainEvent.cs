using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Events;

internal sealed class PaymentStartedDomainEvent : DomainEventBase
{
    public PaymentStartedDomainEvent(Guid paymentId)
    {
        PaymentId = paymentId;
    }

    public Guid PaymentId { get; set; }
}