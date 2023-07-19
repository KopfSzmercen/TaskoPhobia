using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresInvitationRepository : IInvitationRepository
{
    private readonly DbSet<Invitation> _invitations;

    public PostgresInvitationRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _invitations = dbContext.Invitations;
    }

    public async Task<Invitation> FindByIdAsync(InvitationId id)
    {
        return await _invitations.Where(x => x.Id == id)
            .Include(x => x.Project)
            .SingleOrDefaultAsync();
    }

    public Task UpdateAsync(Invitation invitation)
    {
        _invitations.Update(invitation);
        return Task.CompletedTask;
    }
}