using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Invitations;

public class BrowseSentInvitations : IQuery<IEnumerable<SentInvitationDto>>
{
    public BrowseSentInvitations(Guid projectId, Guid userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
}