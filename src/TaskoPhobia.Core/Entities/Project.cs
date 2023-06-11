using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class Project
{
    public ProjectId Id { get; private set; }
    public ProjectName Name { get; private set; }
    public ProjectDescription Description { get; private set; }
    public ProgressStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public UserId OwnerId { get; private set; }
    public User Owner { get; private set; }
    

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
}