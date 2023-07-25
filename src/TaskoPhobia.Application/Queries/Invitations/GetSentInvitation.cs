using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Invitations;

public class GetSentInvitation : IQuery<SentInvitationDto>
{
    public GetSentInvitation(Guid projectId, Guid invitationId)
    {
        ProjectId = projectId;
        InvitationId = invitationId;
    }

    public Guid ProjectId { get; set; }
    public Guid InvitationId { get; set; }
}