using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Invitations;

public class BrowseReceivedInvitations : IQuery<IEnumerable<ReceivedInvitationDto>>
{
    public BrowseReceivedInvitations(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}