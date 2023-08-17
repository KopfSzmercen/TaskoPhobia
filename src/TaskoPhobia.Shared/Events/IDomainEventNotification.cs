using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Shared.Events;

//marker
public interface IDomainEventNotification
{
    Guid Id { get; }
}

public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    where TEventType : IDomainEvent
{
    TEventType DomainEvent { get; }
}