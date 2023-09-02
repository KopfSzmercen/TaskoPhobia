using TaskoPhobia.Core.Entities.Products.ValueObjects;

namespace TaskoPhobia.Core.Entities.Products;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order> FindByIdAsync(OrderId order);
    Task UpdateAsync(Order order);
}