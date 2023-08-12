namespace TaskoPhobia.Shared.Processing;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync();
}