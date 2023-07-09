using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Invitations;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Invitations;

internal sealed class
    BrowseSentInvitationsHandler : IQueryHandler<BrowseSentInvitations, IEnumerable<SentInvitationDto>>
{
    private readonly DbSet<InvitationReadModel> _invitations;

    public BrowseSentInvitationsHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _invitations = dbContext.Invitations;
    }

    public async Task<IEnumerable<SentInvitationDto>> HandleAsync(BrowseSentInvitations query)
    {
        return await _invitations.Where(x => x.ProjectId == query.ProjectId && x.SenderId == query.UserId)
            .Include(x => x.Receiver)
            .Select(x => x.AsSentInvitationDto())
            .ToListAsync();
    }
}