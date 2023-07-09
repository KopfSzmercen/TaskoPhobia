using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Invitations.CreateInvitation;

public record CreateInvitation(Guid InvitationId, Guid SenderId, Guid ReceiverId, Guid ProjectId) : ICommand;