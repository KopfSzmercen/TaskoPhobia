using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.ProjectTasks;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.ProjectTasks;

internal sealed class BrowseProjectTasksHandler : IQueryHandler<BrowseProjectTasks, IEnumerable<ProjectTaskDto>>
{
    private readonly DbSet<ProjectReadModel> _projects;

    public BrowseProjectTasksHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<IEnumerable<ProjectTaskDto>> HandleAsync(BrowseProjectTasks query)
    {
        return await _projects.Include(x => x.Tasks)
            .AsNoTracking()
            .Where(x => x.Id == query.ProjectId &&
                        (x.OwnerId == query.UserId || x.Participations.Any(p => p.ParticipantId == query.UserId)))
            .Include(x => x.Tasks)
            .Select(x => x.Tasks.Select(t => t.AsDto()))
            .SingleOrDefaultAsync();
    }
}