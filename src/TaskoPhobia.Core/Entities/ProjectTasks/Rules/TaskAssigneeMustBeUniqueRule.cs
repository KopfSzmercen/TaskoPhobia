using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks.Rules;

internal sealed class TaskAssigneeMustBeUniqueRule : IBusinessRule
{
    private readonly UserId _newAssigneeId;
    private readonly IEnumerable<TaskAssignment> _taskAssignments;

    public TaskAssigneeMustBeUniqueRule(UserId newAssigneeId, IEnumerable<TaskAssignment> taskAssignments)
    {
        _newAssigneeId = newAssigneeId;
        _taskAssignments = taskAssignments;
    }

    public string Message => "This user is already assigned to this task";

    public bool IsBroken()
    {
        return _taskAssignments.Any(x => x.AssigneeId == _newAssigneeId);
    }
}