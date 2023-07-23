using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Users;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Users;

internal sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, Paged<UserDetailsDto>>
{
    private readonly DbSet<UserReadModel> _users;

    public BrowseUsersHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<Paged<UserDetailsDto>> HandleAsync(BrowseUsers query)
    {
        var users = _users
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Username)) users = users.Where(x => x.Username == query.Username);
        users = Sort(query, users);

        return await users.Select(x => x.AsUserDetailsDto())
            .PaginateAsync(query);
    }

    private static IQueryable<UserReadModel> Sort(BrowseUsers query, IQueryable<UserReadModel> users)
    {
        return query.OrderBy.ToLower() switch
        {
            "username" => query.SortOrder.Equals(IPagedQuery.SortOrderOptions.Asc)
                ? users.OrderBy(x => x.Username)
                : users.OrderByDescending(x => x.Username),
            "email" => query.SortOrder.Equals(IPagedQuery.SortOrderOptions.Asc)
                ? users.OrderBy(x => x.Email)
                : users.OrderByDescending(x => x.Email),
            _ => users.OrderBy(x => x.Id)
        };
    }
}