using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Repositories;

public interface IProjectTaskRepository
{
    Task AddAsync(ProjectTask projectTask);
    Task<ProjectTask> FindByIdAsync(ProjectTaskId projectTaskId);
    Task UpdateAsync(ProjectTask projectTask);
}