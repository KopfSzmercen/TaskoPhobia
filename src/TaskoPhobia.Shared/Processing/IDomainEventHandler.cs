using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Shared.Processing;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}