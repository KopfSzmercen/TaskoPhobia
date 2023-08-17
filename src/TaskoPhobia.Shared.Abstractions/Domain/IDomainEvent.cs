namespace TaskoPhobia.Shared.Abstractions.Domain;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTimeOffset OccuredOn { get; }
}