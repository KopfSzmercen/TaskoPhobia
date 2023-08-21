using TaskoPhobia.Core.DomainServices.Orders.Rules;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Orders;

public class OrdersService : DomainService, IOrdersService
{
    public Order CreateOrderForAccountUpgradeProduct(AccountUpgradeProduct product, User user,
        DateTimeOffset now)
    {
        CheckRule(new AccountUpgradeCanNotBeLowerThanCurrentUserAccount(product, user));
        return Order.NewFromProduct(Guid.NewGuid(), product, user.Id, now);
    }
}