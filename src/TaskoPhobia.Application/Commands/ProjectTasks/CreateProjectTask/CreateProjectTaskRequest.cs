namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;

public class CreateProjectTaskRequest
{
    public string TaskName { get; init; }

    public DateTime Start { get; init; }

    public DateTime End { get; init; }

    public CreateProjectTask ToCommand(Guid userId, Guid projectId)
    {
        return new CreateProjectTask(Guid.NewGuid(), projectId, userId, TaskName, Start, End);
    }
}