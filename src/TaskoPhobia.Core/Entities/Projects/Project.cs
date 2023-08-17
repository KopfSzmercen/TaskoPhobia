using TaskoPhobia.Core.Common.Rules;
using TaskoPhobia.Core.Entities.Projects.Events;
using TaskoPhobia.Core.Entities.Projects.Rules;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.Entities.Projects;

public class Project : Entity
{
    private ProjectSummary ProjectSummary;

    private Project(ProjectId id, ProjectName name, ProjectDescription description,
        ProgressStatus status, DateTime createdAt, UserId ownerId)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        OwnerId = ownerId;

        AddDomainEvent(new ProjectCreatedDomainEvent(Id));
    }

    public Project()
    {
    }

    public ProjectId Id { get; }
    public ProjectName Name { get; private set; }
    public ProjectDescription Description { get; private set; }
    public ProgressStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public UserId OwnerId { get; }
    public User Owner { get; init; }

    internal static Project CreateNew(ProjectId id, ProjectName name, ProjectDescription description,
        IClock clock, UserId ownerId)
    {
        return new Project(id, name, description, ProgressStatus.InProgress(), clock.Now(), ownerId);
    }

    public void Finish(UserId idOfUserWhoWantsToFinishProject, bool allProjectTasksAreFinished)
    {
        CheckRule(new FinishedProjectCanNotBeModifiedRule(this));
        CheckRule(new ProjectCanBeFinishedByItsOwnerRule(idOfUserWhoWantsToFinishProject, OwnerId));
        CheckRule(new ProjectCanBeFinishedIfAllTasksAreFinished(allProjectTasksAreFinished));
        Status = ProgressStatus.Finished();
    }
}