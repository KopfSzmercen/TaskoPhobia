using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Shared.Events;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}