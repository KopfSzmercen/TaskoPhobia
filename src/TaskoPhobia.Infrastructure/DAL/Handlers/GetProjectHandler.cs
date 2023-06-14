using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

internal sealed class GetProjectHandler : IQueryHandler<GetProject, ProjectDto>
{
    private readonly DbSet<Project> _projects;

    public GetProjectHandler(TaskoPhobiaDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }


    public async Task<ProjectDto> HandleAsync(GetProject query)
    {
        var projectId = new ProjectId(query.ProjectId);
        var userId = new UserId(query.UserId);

        var project = await _projects
            .AsNoTracking()
            .Where(x => x.OwnerId == userId && x.Id == projectId)
            .SingleOrDefaultAsync();

        return project?.AsDto();
    }
}