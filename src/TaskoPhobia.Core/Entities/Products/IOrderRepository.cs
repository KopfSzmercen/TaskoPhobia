namespace TaskoPhobia.Core.Entities.Products;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}