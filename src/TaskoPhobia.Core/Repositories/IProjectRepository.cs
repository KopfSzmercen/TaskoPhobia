using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Repositories;

public interface IProjectRepository
{
    Task<Project> FindByIdAsync(ProjectId id);
    Task UpdateAsync(Project project);
}