using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly DbSet<UserReadModel> _users;

    public GetUserHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var user = await _users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == query.UserId);
        return user?.AsDto();
    }
}