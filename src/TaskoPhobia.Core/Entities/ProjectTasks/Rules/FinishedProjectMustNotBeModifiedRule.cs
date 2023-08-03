using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks.Rules;

internal sealed class FinishedProjectMustNotBeModifiedRule : IBusinessRule
{
    private readonly ProjectSummary _project;

    public FinishedProjectMustNotBeModifiedRule(ProjectSummary project)
    {
        _project = project;
    }

    public string Message => "Finished project can not be modified";

    public bool IsBroken()
    {
        return _project.Status.Equals(ProgressStatus.Finished());
    }
}