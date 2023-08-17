using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Invitations.Events;

public class InvitationCreatedDomainEvent : DomainEventBase
{
    public Guid SenderId { get; init; }
    public Guid ReceiverId { get; init; }
}