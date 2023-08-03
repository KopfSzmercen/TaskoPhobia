using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities.ProjectTasks;

public class TaskAssignment
{
    private User Assignee;

    private TaskAssignment(TaskAssignmentId id, UserId assigneeId, ProjectTaskId taskId)
    {
        Id = id;
        AssigneeId = assigneeId;
        CreatedAt = DateTimeOffset.Now;
        TaskId = taskId;
    }


    public TaskAssignmentId Id { get; }
    public DateTimeOffset CreatedAt { get; }
    public ProjectTaskId TaskId { get; }
    public UserId AssigneeId { get; }

    internal static TaskAssignment New(TaskAssignmentId id, UserId assigneeId, ProjectTaskId taskId)
    {
        return new TaskAssignment(id, assigneeId, taskId);
    }
}