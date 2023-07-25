using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Users;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDetailsDto>
{
    private readonly IContext _context;
    private readonly DbSet<UserReadModel> _users;

    public GetUserHandler(TaskoPhobiaReadDbContext dbContext, IContext context)
    {
        _context = context;
        _users = dbContext.Users;
    }

    public async Task<UserDetailsDto> HandleAsync(GetUser query)
    {
        var user = await _users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == _context.Identity.Id);
        return user?.AsUserDetailsDto();
    }
}