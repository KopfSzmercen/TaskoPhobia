using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresProjectTaskRepository : IProjectTaskRepository
{
    private readonly DbSet<ProjectTask> _projectTasks;

    public PostgresProjectTaskRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _projectTasks = dbContext.ProjectTasks;
    }

    public async Task AddAsync(ProjectTask projectTask)
    {
        await _projectTasks.AddAsync(projectTask);
    }
}