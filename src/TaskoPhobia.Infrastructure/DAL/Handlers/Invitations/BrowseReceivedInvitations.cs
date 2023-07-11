using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Invitations;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Invitations;

internal sealed class
    BrowseReceivedInvitationsHandler : IQueryHandler<BrowseReceivedInvitations, IEnumerable<ReceivedInvitationDto>>
{
    private readonly DbSet<InvitationReadModel> _invitations;

    public BrowseReceivedInvitationsHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _invitations = dbContext.Invitations;
    }

    public async Task<IEnumerable<ReceivedInvitationDto>> HandleAsync(BrowseReceivedInvitations query)
    {
        return await _invitations.AsNoTracking()
            .Where(x => x.ReceiverId == query.UserId)
            .Include(x => x.Sender)
            .Select(x => x.AsReceivedInvitationDto())
            .ToListAsync();
    }
}