using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks.Rules;

public class FinishedTaskMustNotBeModifiedRule : IBusinessRule
{
    private readonly ProgressStatus _taskStatus;

    public FinishedTaskMustNotBeModifiedRule(ProgressStatus taskStatus)
    {
        _taskStatus = taskStatus;
    }

    public string Message => "Finished task can not be modified";

    public bool IsBroken()
    {
        return _taskStatus.Equals(ProgressStatus.Finished());
    }
}