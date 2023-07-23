using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Invitations.AcceptInvitation;

public record AcceptInvitation(Guid InvitationId) : ICommand;