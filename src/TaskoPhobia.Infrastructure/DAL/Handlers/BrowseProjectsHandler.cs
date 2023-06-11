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
        var projects = await _projects.Where(x => x.OwnerId == new UserId(query.UserId))
            .AsNoTracking()
            .ToListAsync();

        return projects.Select(x => x.AsDto());
    }
}