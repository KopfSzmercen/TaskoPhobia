namespace TaskoPhobia.Application.Commands.Invitations.CreateInvitation;

public class CreateInvitationRequest
{
    public Guid ReceiverId { get; init; }

    public CreateInvitation ToCommand(Guid projectId)
    {
        return new CreateInvitation(Guid.NewGuid(), ReceiverId, projectId);
    }
}