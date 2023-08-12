using TaskoPhobia.Core.Entities.Projects.Events;
using TaskoPhobia.Shared.Processing;

namespace TaskoPhobia.Application.EventHandlers;

public class LogToConsoleWhenProjectIsCreatedDomainEventHandler : IDomainEventHandler<ProjectCreatedDomainEvent>
{
    public Task HandleAsync(ProjectCreatedDomainEvent domainEvent)
    {
        Console.WriteLine($"Project {domainEvent.ProjectId} has been created");
        return Task.CompletedTask;
    }
}