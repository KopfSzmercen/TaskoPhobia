namespace TaskoPhobia.Core.Entities.AccountUpgradeProducts;

public interface IAccountUpgradeProductRepository
{
    Task<IEnumerable<AccountUpgradeProduct>> FindAllAsync();
    Task AddRangeAsync(IEnumerable<AccountUpgradeProduct> accountUpgradeProducts);
}