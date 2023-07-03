using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

internal sealed class BrowseProjectsHandler : IQueryHandler<BrowseProjects, IEnumerable<ProjectDto>>
{
    private readonly DbSet<Project> _projects;

    public BrowseProjectsHandler(TaskoPhobiaDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<IEnumerable<ProjectDto>> HandleAsync(BrowseProjects query)
    {
        return (await _projects.AsNoTracking().Where(x => x.OwnerId == new UserId(query.UserId))
                .ToListAsync())
            .Select(x => x.AsDto());
    }
}