using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Projects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Projects;

internal sealed class BrowseProjectsHandler : IQueryHandler<BrowseProjects, Paged<ProjectDto>>
{
    private readonly IContext _context;
    private readonly DbSet<ProjectReadModel> _projects;

    public BrowseProjectsHandler(TaskoPhobiaReadDbContext dbContext, IContext context)
    {
        _context = context;
        _projects = dbContext.Projects;
    }

    public async Task<Paged<ProjectDto>> HandleAsync(BrowseProjects query)
    {
        var projects = _projects.AsNoTracking();

        if (query.Created) projects = projects.Where(x => x.OwnerId == _context.Identity.Id);

        else
            projects = projects.Where(x =>
                x.Participations.Any(p => p.ParticipantId == _context.Identity.Id));

        projects = Sort(query, projects);

        return await projects.Select(x => x.AsDto()).PaginateAsync(query);
    }

    private static IQueryable<ProjectReadModel> Sort(BrowseProjects query, IQueryable<ProjectReadModel> projects)
    {
        return query.OrderBy.ToLower() switch
        {
            "name" => query.SortOrder.Equals(IPagedQuery.SortOrderOptions.Asc)
                ? projects.OrderBy(x => x.Name)
                : projects.OrderByDescending(x => x.Name),
            _ => projects.OrderBy(x => x.Id)
        };
    }
}