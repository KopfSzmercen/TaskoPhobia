using TaskoPhobia.Core.Entities.ProjectTasks;

namespace TaskoPhobia.Core.Repositories;

public interface IProjectTaskRepository
{
    Task AddAsync(ProjectTask projectTask);
}