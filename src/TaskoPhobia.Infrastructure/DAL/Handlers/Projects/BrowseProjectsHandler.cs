using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Projects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Projects;

internal sealed class BrowseProjectsHandler : IQueryHandler<BrowseProjects, IEnumerable<ProjectDto>>
{
    private readonly DbSet<ProjectReadModel> _projects;

    public BrowseProjectsHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<IEnumerable<ProjectDto>> HandleAsync(BrowseProjects query)
    {
       return await _projects.AsNoTracking()
           .Where(x => x.OwnerId == query.UserId)
           .Select(x => x.AsDto()).ToListAsync();
    }
}