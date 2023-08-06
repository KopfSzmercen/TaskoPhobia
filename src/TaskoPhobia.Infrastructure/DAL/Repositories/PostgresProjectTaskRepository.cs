using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
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

    public async Task<ProjectTask> FindByIdAsync(ProjectTaskId projectTaskId)
    {
        return await _projectTasks
            .Include(x => x.Assignments)
            .Include(x => x.Project)
            .SingleOrDefaultAsync(x => x.Id == projectTaskId);
    }

    public Task UpdateAsync(ProjectTask projectTask)
    {
        _projectTasks.Update(projectTask);
        return Task.CompletedTask;
    }
}