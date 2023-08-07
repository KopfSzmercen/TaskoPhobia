using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Services;

internal sealed class PostgresProjectParticipationReadService : IProjectParticipationReadService
{
    private readonly DbSet<ProjectParticipation> _projectParticipations;

    public PostgresProjectParticipationReadService(TaskoPhobiaWriteDbContext dbContext)
    {
        _projectParticipations = dbContext.ProjectParticipations;
    }

    public async Task<bool> IsUserProjectParticipantAsync(ProjectId projectId, UserId userId)
    {
        return await _projectParticipations
            .AsNoTracking()
            .AnyAsync(x => x.ProjectId == projectId && x.ParticipantId == userId);
    }
}