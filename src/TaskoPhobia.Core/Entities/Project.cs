using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class Project
{
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
    public UserId OwnerId { get; private set; }
    public User Owner { get; private set; }
    public IEnumerable<ProjectTask> Tasks => _tasks;

    public void AddTask(ProjectTask task)
    {
        if (Status.Equals(ProgressStatus.Finished())) throw new NotAllowedToModifyFinishedProject();
        _tasks.Add(task);
    }
}