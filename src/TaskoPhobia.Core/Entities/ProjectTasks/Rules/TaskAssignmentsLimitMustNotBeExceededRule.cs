using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks.Rules;

internal sealed class TaskAssignmentsLimitMustNotBeExceededRule : IBusinessRule
{
    private readonly int _assignmentsLimit;
    private readonly IEnumerable<TaskAssignment> _taskAssignments;

    public TaskAssignmentsLimitMustNotBeExceededRule(IEnumerable<TaskAssignment> taskAssignments, int assignmentsLimit)
    {
        _taskAssignments = taskAssignments;
        _assignmentsLimit = assignmentsLimit;
    }

    public string Message =>
        $"Can not add assignment. Max number of assignments for this task is {_assignmentsLimit} ";

    public bool IsBroken()
    {
        return _taskAssignments.Count() + 1 > _assignmentsLimit;
    }
}