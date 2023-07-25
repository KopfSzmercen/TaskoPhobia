using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Invitations;

public class BrowseReceivedInvitations : PagedQuery<ReceivedInvitationDto>
{
}