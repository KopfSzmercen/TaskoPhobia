using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.AccountUpgradeProducts;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.AccountUpgradeProducts;

internal sealed class
    BrowseAccountUpgradeProductsHandler : IQueryHandler<BrowseAccountUpgradeProducts,
        IEnumerable<AccountUpgradeProductDto>>
{
    private readonly DbSet<AccountUpgradeProductReadModel> _accountUpgradeProducts;

    public BrowseAccountUpgradeProductsHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _accountUpgradeProducts = dbContext.AccountUpgradeProducts;
    }

    public async Task<IEnumerable<AccountUpgradeProductDto>> HandleAsync(BrowseAccountUpgradeProducts query)
    {
        return await _accountUpgradeProducts
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}