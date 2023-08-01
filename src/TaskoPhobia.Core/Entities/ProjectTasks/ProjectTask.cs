using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks.Rules;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks;

public class ProjectTask : Entity
{
    private ProjectTask(ProjectTaskId id, ProjectTaskName name, TaskTimeSpan timeSpan, ProjectId projectId,
        ProgressStatus status)
    {
        Id = id;
        Name = name;
        TimeSpan = timeSpan;
        ProjectId = projectId;
        Status = status;
    }

    public ProjectTask()
    {
    }

    public ProjectTaskId Id { get; private set; }
    public ProjectTaskName Name { get; private set; }
    public TaskTimeSpan TimeSpan { get; private set; }
    public ProgressStatus Status { get; private set; }
    public Project Project { get; init; }
    public ProjectId ProjectId { get; private set; }

    public static ProjectTask CreateNew(ProjectTaskId id, ProjectTaskName name, TaskTimeSpan timeSpan,
        Project project)
    {
        CheckRule(new CanCreateTaskIfProjectIsNotFinished(project));
        return new ProjectTask(id, name, timeSpan, project.Id, ProgressStatus.InProgress());
    }
}