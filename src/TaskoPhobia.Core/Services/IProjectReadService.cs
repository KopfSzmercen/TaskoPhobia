using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Services;

public interface IProjectReadService
{
    Task<int> CountOwnedByUserAsync(UserId ownerId);
    Task<bool> CheckAllTasksAreFinishedAsync(ProjectId projectId);
}