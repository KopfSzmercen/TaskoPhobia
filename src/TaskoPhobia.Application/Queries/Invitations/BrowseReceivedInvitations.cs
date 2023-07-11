using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Invitations;

public record BrowseReceivedInvitations(Guid UserId) : IQuery<IEnumerable<ReceivedInvitationDto>>
{
}