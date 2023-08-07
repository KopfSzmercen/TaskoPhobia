using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Projects.Rules;

internal sealed class ProjectCanBeFinishedIfAllTasksAreFinished : IBusinessRule
{
    private readonly bool _allTasksAreFinished;

    public ProjectCanBeFinishedIfAllTasksAreFinished(bool allTasksAreFinished)
    {
        _allTasksAreFinished = allTasksAreFinished;
    }

    public string Message => "Project can't be finished unless all its tasks are finished";

    public bool IsBroken()
    {
        return !_allTasksAreFinished;
    }
}