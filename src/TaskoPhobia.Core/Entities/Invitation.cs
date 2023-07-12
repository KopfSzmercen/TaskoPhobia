using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class Invitation
{
    private Invitation(InvitationId id, InvitationTitle title, UserId senderId, UserId receiverId,
        InvitationStatus status, DateTime createdAt)
    {
        Id = id;
        Title = title;
        SenderId = senderId;
        ReceiverId = receiverId;
        Status = status;
        CreatedAt = createdAt;
    }

    public Invitation()
    {
    }

    public InvitationId Id { get; }
    public InvitationTitle Title { get; }
    public UserId SenderId { get; }
    public UserId ReceiverId { get; }
    public InvitationStatus Status { get; private set; }
    public User Sender { get; init; }
    public User Receiver { get; init; }
    public ProjectId ProjectId { get; init; }
    public Project Project { get; init; }
    public DateTime CreatedAt { get; }

    public static Invitation CreateNew(InvitationId id, InvitationTitle title, UserId senderId, UserId receiverId,
        DateTime createdAt)
    {
        return new Invitation(id, title, senderId, receiverId, InvitationStatus.Pending(), createdAt);
    }

    internal void Accept()
    {
        if (Status == InvitationStatus.Accepted() || Status == InvitationStatus.Declined())
            throw new InvitationCanNotBeAcceptedException();

        Status = InvitationStatus.Accepted();
    }
}