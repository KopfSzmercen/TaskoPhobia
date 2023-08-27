using TaskoPhobia.Core.Entities.Products.ValueObjects;

namespace TaskoPhobia.Core.Entities.AccountUpgradeProducts;

public interface IAccountUpgradeProductRepository
{
    Task<IEnumerable<AccountUpgradeProduct>> FindAllAsync();
    Task AddRangeAsync(IEnumerable<AccountUpgradeProduct> accountUpgradeProducts);
    Task<AccountUpgradeProduct> FindByIdAsync(ProductId productId);
}