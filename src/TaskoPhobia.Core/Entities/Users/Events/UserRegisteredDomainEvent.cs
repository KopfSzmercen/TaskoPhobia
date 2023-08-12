using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Users.Events;

public class UserRegisteredDomainEvent : DomainEventBase
{
    public UserRegisteredDomainEvent(UserId userId)
    {
        UserId = userId;
    }

    public UserId UserId { get; }
}