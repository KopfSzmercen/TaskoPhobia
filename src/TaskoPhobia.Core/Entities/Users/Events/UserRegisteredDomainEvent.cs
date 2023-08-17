using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Users.Events;

public class UserRegisteredDomainEvent : DomainEventBase
{
    public UserRegisteredDomainEvent(UserId userId, Email email)
    {
        UserId = userId;
        Email = email;
    }

    public Guid UserId { get; }
    public string Email { get; }
}