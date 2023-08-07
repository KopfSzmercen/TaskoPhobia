using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Services;

internal sealed class PostgresProjectReadService : IProjectReadService
{
    private readonly DbSet<Project> _projects;
    private readonly DbSet<ProjectTask> _projectTasks;

    public PostgresProjectReadService(TaskoPhobiaWriteDbContext dbContext)
    {
        _projectTasks = dbContext.ProjectTasks;
        _projects = dbContext.Projects;
    }

    public async Task<int> CountOwnedByUserAsync(UserId ownerId)
    {
        return await _projects.AsNoTracking().CountAsync(x => x.OwnerId == ownerId);
    }

    public async Task<bool> CheckAllTasksAreFinishedAsync(ProjectId projectId)
    {
        return await _projectTasks
            .AsNoTracking()
            .Where(x => x.ProjectId == projectId)
            .AllAsync(x => x.Status == ProgressStatus.Finished());
    }
}