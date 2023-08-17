using TaskoPhobia.Core.Entities.AccountUpgradeProducts;

namespace TaskoPhobia.Core.DomainServices.AccountUpgradeProducts;

public interface IAccountUpgradeProductService
{
    IEnumerable<AccountUpgradeProduct> CreateInitialProducts(
        IEnumerable<AccountUpgradeProduct> existingAccountUpgradeProducts);
}