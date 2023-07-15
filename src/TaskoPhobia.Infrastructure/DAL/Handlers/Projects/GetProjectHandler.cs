using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Projects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectHandler : IQueryHandler<GetProject, ProjectDetailsDto>
{
    private readonly DbSet<ProjectReadModel> _projects;

    public GetProjectHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }


    public async Task<ProjectDetailsDto> HandleAsync(GetProject query)
    {
        var project = await _projects
            .AsNoTracking()
            .Where(x =>
                x.Id == query.ProjectId &&
                (x.OwnerId == query.UserId || x.Participations.Any(p => p.ParticipantId == query.UserId)))
            .Include(x => x.Participations)
            .ThenInclude(p => p.User)
            .SingleOrDefaultAsync();

        return project?.AsProjectDetailsDto();
    }
}