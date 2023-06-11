using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresProjectRepository : IProjectRepository
{
    private readonly DbSet<Project> _projects;

    public PostgresProjectRepository(TaskoPhobiaDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }


    public async Task<Project> FindByIdAsync(ProjectId id)
    {
        return await _projects.Include(x => x.Tasks).SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task UpdateAsync(Project project)
    {
        _projects.Update(project);
        return Task.CompletedTask;
    }
}