﻿using TaskoPhobia.Core.Entities.Invitations.Events;
using TaskoPhobia.Core.Entities.Invitations.Rules;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.Entities.Invitations;

public class Invitation : Entity
{
    private Invitation(InvitationId id, ProjectId projectId, InvitationTitle title, UserId senderId, UserId receiverId,
        InvitationStatus status, DateTime createdAt)
    {
        Id = id;
        Title = title;
        SenderId = senderId;
        ReceiverId = receiverId;
        Status = status;
        CreatedAt = createdAt;
        ProjectId = projectId;

        AddDomainEvent(new InvitationCreatedDomainEvent
        {
            ReceiverId = receiverId,
            SenderId = senderId
        });
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
    public bool BlockSendingMoreInvitations { get; private set; }

    internal static Invitation CreateNew(InvitationId id, ProjectId projectId, InvitationTitle title, UserId senderId,
        UserId receiverId,
        IClock clock)
    {
        var invitation = new Invitation(id, projectId, title, senderId, receiverId,
            InvitationStatus.Pending(), clock.Now());

        return invitation;
    }

    internal void Accept()
    {
        CheckRule(new OnlyPendingInvitationStatusCanBeChangedRule(this));
        Status = InvitationStatus.Accepted();
    }

    public void Reject(bool blockSendingMoreInvitations)
    {
        CheckRule(new OnlyPendingInvitationStatusCanBeChangedRule(this));

        Status = InvitationStatus.Rejected();
        BlockSendingMoreInvitations = blockSendingMoreInvitations;
    }
}