using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresAccountUpgradeProductRepository : IAccountUpgradeProductRepository
{
    private readonly DbSet<AccountUpgradeProduct> _accountUpgradeProducts;
    private readonly DbSet<Product> _products;

    public PostgresAccountUpgradeProductRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _accountUpgradeProducts = dbContext.AccountUpgradeProducts;
        _products = dbContext.Products;
    }

    public async Task<IEnumerable<AccountUpgradeProduct>> FindAllAsync()
    {
        var products = await _products.ToListAsync();
        return await _accountUpgradeProducts.ToListAsync();
    }

    public async Task AddRangeAsync(IEnumerable<AccountUpgradeProduct> accountUpgradeProducts)
    {
        await _accountUpgradeProducts.AddRangeAsync(accountUpgradeProducts);
    }

    public async Task<AccountUpgradeProduct> FindByIdAsync(ProductId productId)
    {
        return await _accountUpgradeProducts
            .Where(x => x.Id == productId)
            .SingleOrDefaultAsync();
    }
}