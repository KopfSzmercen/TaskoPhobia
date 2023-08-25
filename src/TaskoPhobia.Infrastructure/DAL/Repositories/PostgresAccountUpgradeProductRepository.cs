using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresAccountUpgradeProductRepository : IAccountUpgradeProductRepository
{
    private readonly DbSet<AccountUpgradeProduct> _accountUpgradeProducts;
    private readonly DbSet<Project> _projects;

    public PostgresAccountUpgradeProductRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _accountUpgradeProducts = dbContext.AccountUpgradeProducts;
        _projects = dbContext.Projects;
    }

    public async Task<IEnumerable<AccountUpgradeProduct>> FindAllAsync()
    {
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