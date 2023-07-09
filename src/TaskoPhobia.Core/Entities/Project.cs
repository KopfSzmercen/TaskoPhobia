using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class Project
{
    private readonly ICollection<Invitation> _invitations = new List<Invitation>();
    private readonly ICollection<ProjectTask> _tasks = new List<ProjectTask>();


    public Project(ProjectId id, ProjectName name, ProjectDescription description,
        ProgressStatus status, DateTime createdAt, UserId ownerId)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        OwnerId = ownerId;
    }

    public ProjectId Id { get; private set; }
    public ProjectName Name { get; private set; }
    public ProjectDescription Description { get; private set; }
    public ProgressStatus Status { get; }
    public DateTime CreatedAt { get; private set; }
    public UserId OwnerId { get; }
    public User Owner { get; init; }
    public IEnumerable<ProjectTask> Tasks => _tasks;
    public IEnumerable<Invitation> Invitations => _invitations;

    public void AddTask(ProjectTask task)
    {
        if (Status.Equals(ProgressStatus.Finished())) throw new NotAllowedToModifyFinishedProject();
        _tasks.Add(task);
    }

    public void AddInvitation(Invitation invitation)
    {
        if (_invitations.Any(i => i.ReceiverId == invitation.ReceiverId && i.Status == InvitationStatus.Pending()))
            throw new InvitationAlreadySentException(invitation.ReceiverId);

        if (OwnerId != invitation.SenderId) throw new NotAllowedToCreateInvitation();

        _invitations.Add(invitation);
    }
}