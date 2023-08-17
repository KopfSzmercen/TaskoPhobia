using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Shared.Events;

public interface IDomainNotificationHandler<in TEvent>
    where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent notification);
}