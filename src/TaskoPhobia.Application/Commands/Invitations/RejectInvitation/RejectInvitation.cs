using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Invitations.RejectInvitation;

public record RejectInvitation(Guid InvitationId, Guid ReceiverId) : ICommand;