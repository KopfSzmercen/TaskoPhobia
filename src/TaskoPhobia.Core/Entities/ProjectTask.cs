using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class ProjectTask
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
        ProjectId projectId)
    {
        return new ProjectTask(id, name, timeSpan, projectId, ProgressStatus.InProgress());
    }
}