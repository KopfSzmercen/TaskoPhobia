using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.ProjectTasks;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.ProjectTasks;

internal sealed class BrowseProjectTasksHandler : IQueryHandler<BrowseProjectTasks, Paged<ProjectTaskDto>>
{
    private readonly IContext _context;
    private readonly DbSet<ProjectTaskReadModel> _projectTasks;

    public BrowseProjectTasksHandler(TaskoPhobiaReadDbContext dbContext, IContext context)
    {
        _context = context;
        _projectTasks = dbContext.ProjectTasks;
    }

    public async Task<Paged<ProjectTaskDto>> HandleAsync(BrowseProjectTasks query)
    {
        var projectTasks = _projectTasks.AsNoTracking().Where(x =>
            x.Project.Id == query.ProjectId &&
            (x.Project.OwnerId == _context.Identity.Id ||
             x.Project.Participations.Any(p =>
                 p.ParticipantId == _context.Identity.Id)));

        projectTasks = Sort(query, projectTasks);

        return await projectTasks.Select(x => x.AsDto()).PaginateAsync(query);
    }

    private static IQueryable<ProjectTaskReadModel> Sort(BrowseProjectTasks query,
        IQueryable<ProjectTaskReadModel> projectTasks)
    {
        return query.OrderBy?.ToLower() switch
        {
            _ => projectTasks.OrderBy(x => x.Id)
        };
    }
}