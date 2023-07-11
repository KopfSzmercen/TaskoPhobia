using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Projects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectHandler : IQueryHandler<GetProject, ProjectDto>
{
    private readonly DbSet<ProjectReadModel> _projects;

    public GetProjectHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }


    public async Task<ProjectDto> HandleAsync(GetProject query)
    {
        
        var project = await _projects
            .AsNoTracking()
            .Where(x => x.OwnerId == query.UserId && x.Id == query.ProjectId)
            .SingleOrDefaultAsync();

        return project?.AsDto();
    }
}