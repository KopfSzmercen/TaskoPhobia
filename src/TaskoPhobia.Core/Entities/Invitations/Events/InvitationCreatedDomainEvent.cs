using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Invitations.Events;

public class InvitationCreatedDomainEvent : DomainEventBase
{
    public UserId SenderId { get; init; }
    public UserId ReceiverId { get; init; }
}