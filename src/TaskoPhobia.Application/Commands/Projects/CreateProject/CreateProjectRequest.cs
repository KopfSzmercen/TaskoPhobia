namespace TaskoPhobia.Application.Commands.Projects.CreateProject;

public class CreateProjectRequest
{
    public string ProjectName { get; init; }
    public string ProjectDescription { get; init; }

    public CreateProject ToCommand()
    {
        return new CreateProject(Guid.NewGuid(), ProjectName, ProjectDescription);
    }
}