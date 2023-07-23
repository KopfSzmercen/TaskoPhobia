using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Projects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectHandler : IQueryHandler<GetProject, ProjectDetailsDto>
{
    private readonly IContext _context;
    private readonly DbSet<ProjectReadModel> _projects;

    public GetProjectHandler(TaskoPhobiaReadDbContext dbContext, IContext context)
    {
        _context = context;
        _projects = dbContext.Projects;
    }


    public async Task<ProjectDetailsDto> HandleAsync(GetProject query)
    {
        var project = await _projects
            .AsNoTracking()
            .Where(x =>
                x.Id == query.ProjectId &&
                (x.OwnerId == _context.Identity.Id ||
                 x.Participations.Any(p => p.ParticipantId == _context.Identity.Id)))
            .Include(x => x.Participations)
            .ThenInclude(p => p.User)
            .SingleOrDefaultAsync();

        return project?.AsProjectDetailsDto();
    }
}