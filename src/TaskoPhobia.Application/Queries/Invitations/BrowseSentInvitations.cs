using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Invitations;

public class BrowseSentInvitations : PagedQuery<SentInvitationDto>
{
    public Guid ProjectId { get; init; }
}