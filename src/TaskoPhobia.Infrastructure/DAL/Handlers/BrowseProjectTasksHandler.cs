using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

internal sealed class BrowseProjectTasksHandler : IQueryHandler<BrowseProjectTasks, IEnumerable<ProjectTaskDto>>
{
    private readonly DbSet<Project> _projects;

    public BrowseProjectTasksHandler(TaskoPhobiaDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }
    public async Task<IEnumerable<ProjectTaskDto>> HandleAsync(BrowseProjectTasks query)
    {
        var projectId = new ProjectId(query.ProjectId);
        var userId = new UserId(query.UserId);

        var project = await _projects.Include(x => x.Tasks)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == projectId && x.OwnerId == userId);

        return project?.Tasks.Select(x => x.AsDto());
    }
}