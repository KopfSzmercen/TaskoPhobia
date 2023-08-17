using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Projects.Events;

public class ProjectCreatedDomainEvent : DomainEventBase
{
    public ProjectCreatedDomainEvent(ProjectId projectId)
    {
        ProjectId = projectId;
    }

    public Guid ProjectId { get; init; }
}