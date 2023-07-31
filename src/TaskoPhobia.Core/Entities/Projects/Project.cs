using TaskoPhobia.Core.DomainServices.Invitations.Rules;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.Entities.Projects;

public class Project : Entity
{
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
    public IEnumerable<ProjectParticipation> Participations => _participations;

    internal static Project CreateNew(ProjectId id, ProjectName name, ProjectDescription description,
        IClock clock, UserId ownerId)
    {
        return new Project(id, name, description, ProgressStatus.InProgress(), clock.Now(), ownerId);
    }

    public void AddTask(ProjectTask task)
    {
        CheckRule(new FinishedProjectCanNotBeModifiedRule(this));
        _tasks.Add(task);
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