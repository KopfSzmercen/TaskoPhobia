namespace TaskoPhobia.Application.Commands.Invitations.CreateInvitation;

public class CreateInvitationRequest
{
    public Guid ReceiverId { get; init; }

    public CreateInvitation ToCommand(Guid projectId, Guid senderId)
    {
        return new CreateInvitation(Guid.NewGuid(), senderId, ReceiverId, projectId);
    }
}