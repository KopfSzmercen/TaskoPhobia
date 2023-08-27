using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresOrderRepository : IOrderRepository
{
    private readonly DbSet<Order> _orders;

    public PostgresOrderRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _orders = dbContext.Orders;
    }

    public async Task AddAsync(Order order)
    {
        await _orders.AddAsync(order);
    }
}