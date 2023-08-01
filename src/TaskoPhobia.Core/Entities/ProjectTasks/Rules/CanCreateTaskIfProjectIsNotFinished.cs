using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks.Rules;

internal sealed class CanCreateTaskIfProjectIsNotFinished : IBusinessRule
{
    private readonly Project _project;

    public CanCreateTaskIfProjectIsNotFinished(Project project)
    {
        _project = project;
    }

    public string Message => "Can not add task to a finished project";

    public bool IsBroken()
    {
        return _project.Status.Equals(ProgressStatus.Finished());
    }
}