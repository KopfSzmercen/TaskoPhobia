using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Common.Rules;

public class FinishedProjectCanNotBeModifiedRule : IBusinessRule
{
    private readonly ProgressStatus _progressStatus;

    public FinishedProjectCanNotBeModifiedRule(Project project)
    {
        _progressStatus = project.Status;
    }

    public FinishedProjectCanNotBeModifiedRule(ProjectSummary project)
    {
        _progressStatus = project.Status;
    }

    public string Message => "Finished project can not be modified";

    public bool IsBroken()
    {
        return _progressStatus.Equals(ProgressStatus.Finished());
    }
}