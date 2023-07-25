using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Invitations;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Invitations;

internal sealed class
    BrowseReceivedInvitationsHandler : IQueryHandler<BrowseReceivedInvitations, Paged<ReceivedInvitationDto>>
{
    private readonly IContext _context;
    private readonly DbSet<InvitationReadModel> _invitations;

    public BrowseReceivedInvitationsHandler(TaskoPhobiaReadDbContext dbContext, IContext context)
    {
        _context = context;
        _invitations = dbContext.Invitations;
    }

    public async Task<Paged<ReceivedInvitationDto>> HandleAsync(BrowseReceivedInvitations query)
    {
        var invitations = _invitations.AsNoTracking()
            .Where(x => x.ReceiverId == _context.Identity.Id);

        invitations = Sort(query, invitations);

        invitations = invitations
            .Include(x => x.Sender)
            .Include(x => x.Project);

        return await invitations.Select(x => x.AsReceivedInvitationDto())
            .PaginateAsync(query);
    }

    private static IQueryable<InvitationReadModel> Sort(BrowseReceivedInvitations query,
        IQueryable<InvitationReadModel> invitations)
    {
        return query.OrderBy.ToLower() switch
        {
            _ => invitations.OrderBy(x => x.Id)
        };
    }
}