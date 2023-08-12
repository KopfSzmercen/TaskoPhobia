namespace TaskoPhobia.Shared.Events;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync();
}