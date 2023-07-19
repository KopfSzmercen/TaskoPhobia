using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Projects.Rules;

public class FinishedProjectCanNotBeModifiedRule : IBusinessRule
{
    private readonly Project _project;

    public FinishedProjectCanNotBeModifiedRule(Project project)
    {
        _project = project;
    }

    public string Message => "Finished project can not be modified";

    public bool IsBroken()
    {
        return _project.Status.Equals(ProgressStatus.Finished());
    }
}