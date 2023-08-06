namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;

public class CreateProjectTaskRequest
{
    public string TaskName { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public int AssignmentsLimit { get; init; }

    public CreateProjectTask ToCommand(Guid projectId)
    {
        return new CreateProjectTask(Guid.NewGuid(), projectId, TaskName, Start, End, AssignmentsLimit);
    }
}