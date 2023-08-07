using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Projects.Rules;

internal sealed class ProjectCanBeFinishedByItsOwnerRule : IBusinessRule
{
    private readonly UserId _idOfUserWhoWantsToFinishProject;
    private readonly UserId _projectOwnerId;

    public ProjectCanBeFinishedByItsOwnerRule(UserId idOfUserWhoWantsToFinishProject, UserId projectOwnerId)
    {
        _idOfUserWhoWantsToFinishProject = idOfUserWhoWantsToFinishProject;
        _projectOwnerId = projectOwnerId;
    }

    public string Message => "You can't finish project unless you are its owner";

    public bool IsBroken()
    {
        return _projectOwnerId != _idOfUserWhoWantsToFinishProject;
    }
}