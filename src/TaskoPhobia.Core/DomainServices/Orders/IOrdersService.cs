using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Users;

namespace TaskoPhobia.Core.DomainServices.Orders;

public interface IOrdersService
{
    Order CreateOrderForAccountUpgradeProduct(AccountUpgradeProduct product, User user,
        DateTimeOffset now);
}