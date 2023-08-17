using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresAccountUpgradeProductRepository : IAccountUpgradeProductRepository
{
    private readonly DbSet<AccountUpgradeProduct> _accountUpgradeProducts;

    public PostgresAccountUpgradeProductRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _accountUpgradeProducts = dbContext.AccountUpgradeProducts;
    }

    public async Task<IEnumerable<AccountUpgradeProduct>> FindAllAsync()
    {
        return await _accountUpgradeProducts.ToListAsync();
    }

    public async Task AddRangeAsync(IEnumerable<AccountUpgradeProduct> accountUpgradeProducts)
    {
        await _accountUpgradeProducts.AddRangeAsync(accountUpgradeProducts);
    }
}