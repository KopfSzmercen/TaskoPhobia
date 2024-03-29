﻿using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresProjectRepository : IProjectRepository
{
    private readonly DbSet<Project> _projects;

    public PostgresProjectRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }


    public async Task<Project> FindByIdAsync(ProjectId id)
    {
        return await _projects
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task UpdateAsync(Project project)
    {
        _projects.Update(project);
        return Task.CompletedTask;
    }

    public async Task AddAsync(Project project)
    {
        await _projects.AddAsync(project);
    }
}