using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Services;

internal sealed class PostgresProjectReadService : IProjectReadService
{
    private readonly DbSet<Project> _projects;

    public PostgresProjectReadService(TaskoPhobiaWriteDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<int> CountOwnedByUserAsync(UserId ownerId)
    {
        return await _projects.CountAsync(x => x.OwnerId == ownerId);
    }
}