using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Invitations;

public class GetSentInvitation : IQuery<SentInvitationDto>
{
    public GetSentInvitation(Guid userId, Guid projectId, Guid invitationId)
    {
        UserId = userId;
        ProjectId = projectId;
        InvitationId = invitationId;
    }

    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid InvitationId { get; set; }
}