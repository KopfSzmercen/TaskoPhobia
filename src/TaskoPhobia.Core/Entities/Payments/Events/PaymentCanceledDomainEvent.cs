using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Events;

internal sealed class PaymentCanceledDomainEvent : DomainEventBase
{
    public PaymentCanceledDomainEvent(Guid paymentId)
    {
        PaymentId = paymentId;
    }

    public Guid PaymentId { get; set; }
}