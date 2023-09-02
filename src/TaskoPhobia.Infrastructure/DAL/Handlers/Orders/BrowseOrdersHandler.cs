using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Orders;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Orders;

internal sealed class BrowseOrdersHandler : IQueryHandler<BrowseOrders, IEnumerable<OrderDto>>
{
    private readonly IContext _context;
    private readonly DbSet<OrderReadModel> _orders;

    public BrowseOrdersHandler(IContext context, TaskoPhobiaReadDbContext readDbContext)
    {
        _context = context;
        _orders = readDbContext.Orders;
    }

    public async Task<IEnumerable<OrderDto>> HandleAsync(BrowseOrders query)
    {
        return await _orders
            .Where(x => x.CustomerId == _context.Identity.Id)
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}