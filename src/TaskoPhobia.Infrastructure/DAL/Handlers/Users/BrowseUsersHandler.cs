using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Users;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Users;

internal sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, IEnumerable<UserDetailsDto>>
{
    private readonly DbSet<UserReadModel> _users;

    public BrowseUsersHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<IEnumerable<UserDetailsDto>> HandleAsync(BrowseUsers query)
    {
        return await _users.AsNoTracking().Select(x => x.AsUserDetailsDto()).ToListAsync();
    }
}