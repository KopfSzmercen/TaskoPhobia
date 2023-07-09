using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class ProjectTask
{
    public ProjectTask()
    {
    }

    public ProjectTask(ProjectTaskId id, ProjectTaskName name, TaskTimeSpan timeSpan, ProjectId projectId,
        ProgressStatus status)
    {
        Id = id;
        Name = name;
        TimeSpan = timeSpan;
        ProjectId = projectId;
        Status = status;
    }

    public ProjectTaskId Id { get; private set; }
    public ProjectTaskName Name { get; private set; }
    public TaskTimeSpan TimeSpan { get; private set; }
    public ProgressStatus Status { get; private set; }
    public Project Project { get; init; }
    public ProjectId ProjectId { get; private set; }
}