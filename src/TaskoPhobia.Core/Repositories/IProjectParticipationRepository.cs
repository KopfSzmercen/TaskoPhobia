using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Core.Repositories;

public interface IProjectParticipationRepository
{
    Task AddAsync(ProjectParticipation projectParticipation);
}