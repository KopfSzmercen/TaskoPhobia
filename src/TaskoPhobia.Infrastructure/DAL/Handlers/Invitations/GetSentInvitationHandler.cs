using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Invitations;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Invitations;

internal sealed class GetSentInvitationHandler : IQueryHandler<GetSentInvitation, SentInvitationDto>
{
    private readonly DbSet<InvitationReadModel> _invitations;

    public GetSentInvitationHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _invitations = dbContext.Invitations;
    }

    public async Task<SentInvitationDto> HandleAsync(GetSentInvitation query)
    {
        return await _invitations.AsNoTracking()
            .Where(x =>
                x.Id == query.InvitationId
                && x.SenderId == query.UserId
                && x.ProjectId == query.ProjectId)
            .Include(x => x.Receiver)
            .Select(x => x.AsSentInvitationDto())
            .SingleOrDefaultAsync();
    }
}