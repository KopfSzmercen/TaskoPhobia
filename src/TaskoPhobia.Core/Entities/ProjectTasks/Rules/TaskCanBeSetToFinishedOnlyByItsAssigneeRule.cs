using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks.Rules;

internal sealed class TaskCanBeSetToFinishedOnlyByItsAssigneeRule : IBusinessRule
{
    private readonly IEnumerable<TaskAssignment> _assignments;
    private readonly UserId _idOfUserWhoSetsTaskStatusToFinished;

    public TaskCanBeSetToFinishedOnlyByItsAssigneeRule(IEnumerable<TaskAssignment> assignments,
        UserId idOfUserWhoSetsTaskStatusToFinished)
    {
        _assignments = assignments;
        _idOfUserWhoSetsTaskStatusToFinished = idOfUserWhoSetsTaskStatusToFinished;
    }

    public string Message => "You can't finish the task unless you are its assignee";

    public bool IsBroken()
    {
        var userWhoSetsTaskStatusToFinishedIsItsParticipant =
            _assignments.Any(x => x.AssigneeId == _idOfUserWhoSetsTaskStatusToFinished);

        return userWhoSetsTaskStatusToFinishedIsItsParticipant is not true;
    }
}