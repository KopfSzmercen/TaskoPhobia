using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Invitations.RejectInvitation;

public record RejectInvitation(Guid InvitationId, bool BlockSendingMoreInvitations) : ICommand;