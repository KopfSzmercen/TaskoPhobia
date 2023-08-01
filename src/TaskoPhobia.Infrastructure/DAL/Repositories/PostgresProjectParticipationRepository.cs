using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresProjectParticipationRepository : IProjectParticipationRepository
{
    private readonly DbSet<ProjectParticipation> _projectParticipations;

    public PostgresProjectParticipationRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _projectParticipations = dbContext.ProjectParticipations;
    }

    public async Task AddAsync(ProjectParticipation projectParticipation)
    {
        await _projectParticipations.AddAsync(projectParticipation);
    }
}