using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Infrastructure.Events;

public interface IDomainEventsAccessor
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();
    void ClearAllDomainEvents();
}