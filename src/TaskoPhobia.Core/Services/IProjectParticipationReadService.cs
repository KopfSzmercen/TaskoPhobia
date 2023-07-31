using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Services;

public interface IProjectParticipationReadService
{
    Task<bool> IsUserProjectParticipantAsync(ProjectId projectId, UserId userId);
}