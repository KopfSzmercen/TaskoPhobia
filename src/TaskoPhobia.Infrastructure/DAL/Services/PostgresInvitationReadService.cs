using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Services;

internal sealed class PostgresInvitationReadService : IInvitationReadService
{
    private readonly DbSet<Invitation> _invitations;

    public PostgresInvitationReadService(TaskoPhobiaWriteDbContext dbContext)
    {
        _invitations = dbContext.Invitations;
    }

    public async Task<IEnumerable<Invitation>> GetInvitationsToProjectForReceiver(ProjectId projectId,
        UserId receiverId)
    {
        return await _invitations
            .Where(x => x.ProjectId == projectId && x.ReceiverId == receiverId)
            .ToListAsync();
    }
}