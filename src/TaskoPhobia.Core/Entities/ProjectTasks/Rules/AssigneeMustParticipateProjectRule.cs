using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.ProjectTasks.Rules;

internal class AssigneeMustParticipateProjectRule : IBusinessRule
{
    private readonly bool _assigneeParticipatesProject;

    public AssigneeMustParticipateProjectRule(bool assigneeParticipatesProject)
    {
        _assigneeParticipatesProject = assigneeParticipatesProject;
    }

    public string Message => "You can't assign a user who doesn't participate project to its task";

    public bool IsBroken()
    {
        return !_assigneeParticipatesProject;
    }
}