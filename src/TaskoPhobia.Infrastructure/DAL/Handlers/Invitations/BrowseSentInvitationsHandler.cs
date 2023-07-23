using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Invitations;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Invitations;

internal sealed class
    BrowseSentInvitationsHandler : IQueryHandler<BrowseSentInvitations, Paged<SentInvitationDto>>
{
    private readonly IContext _context;
    private readonly DbSet<InvitationReadModel> _invitations;

    public BrowseSentInvitationsHandler(TaskoPhobiaReadDbContext dbContext, IContext context)
    {
        _context = context;
        _invitations = dbContext.Invitations;
    }

    public async Task<Paged<SentInvitationDto>> HandleAsync(BrowseSentInvitations query)
    {
        var sentInvitations =
            _invitations.Where(x => x.ProjectId == query.ProjectId && x.SenderId == _context.Identity.Id);

        sentInvitations = Sort(query, sentInvitations);

        return await sentInvitations.Include(x => x.Receiver)
            .Select(x => x.AsSentInvitationDto())
            .PaginateAsync(query);
    }

    private static IQueryable<InvitationReadModel> Sort(BrowseSentInvitations query,
        IQueryable<InvitationReadModel> sentInvitations)
    {
        return query.OrderBy?.ToLower() switch
        {
            _ => sentInvitations?.OrderBy(x => x.Id)
        };
    }
}