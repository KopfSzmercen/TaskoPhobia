﻿using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Projects.Rules;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.Entities.Projects;

public class Project : Entity
{
    private readonly ICollection<Invitation> _invitations = new List<Invitation>();
    private readonly HashSet<ProjectParticipation> _participations = new();
    private readonly ICollection<ProjectTask> _tasks = new List<ProjectTask>();

    private Project(ProjectId id, ProjectName name, ProjectDescription description,
        ProgressStatus status, DateTime createdAt, UserId ownerId)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        OwnerId = ownerId;
    }

    public Project()
    {
    }

    public ProjectId Id { get; private set; }
    public ProjectName Name { get; private set; }
    public ProjectDescription Description { get; private set; }
    public ProgressStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public UserId OwnerId { get; }
    public User Owner { get; init; }
    public IEnumerable<ProjectTask> Tasks => _tasks;
    public IEnumerable<Invitation> Invitations => _invitations;
    public IEnumerable<ProjectParticipation> Participations => _participations;

    public static Project CreateNew(ProjectId id, ProjectName name, ProjectDescription description,
        IClock clock, UserId ownerId)
    {
        return new Project(id, name, description, ProgressStatus.InProgress(), clock.Now(), ownerId);
    }

    public void AddTask(ProjectTask task)
    {
        CheckRule(new FinishedProjectCanNotBeModifiedRule(this));
        _tasks.Add(task);
    }

    public void AddInvitation(Invitation invitation)
    {
        CheckRule(new FinishedProjectCanNotBeModifiedRule(this));

        CheckRule(new BlockedSendingMoreInvitationsRule(this, invitation));
        CheckRule(new InvitationIsAlreadySentToUserRule(this, invitation));
        CheckRule(new InvitationSenderMustBeProjectOwnerRule(this, invitation));
        CheckRule(new ReceiverMustNotParticipateProjectRule(this, invitation));
        CheckRule(new RejectedInvitationsLimitIsNotExceededRule(this, invitation));

        _invitations.Add(invitation);
    }

    public void SetStatusToFinished()
    {
        Status = ProgressStatus.Finished();
    }

    public void AddParticipation(ProjectParticipation participation)
    {
        CheckRule(new FinishedProjectCanNotBeModifiedRule(this));
        _participations.Add(participation);
    }
}