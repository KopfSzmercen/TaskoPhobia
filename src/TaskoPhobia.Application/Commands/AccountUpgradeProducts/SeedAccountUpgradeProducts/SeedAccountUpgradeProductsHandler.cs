using TaskoPhobia.Core.DomainServices.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.AccountUpgradeProducts.SeedAccountUpgradeProducts;

internal sealed class SeedAccountUpgradeProductsHandler : ICommandHandler<SeedAccountUpgradeProducts>
{
    private readonly IAccountUpgradeProductRepository _accountUpgradeProductRepository;
    private readonly IAccountUpgradeProductService _accountUpgradeProductService;

    public SeedAccountUpgradeProductsHandler(IAccountUpgradeProductRepository accountUpgradeProductRepository,
        IAccountUpgradeProductService accountUpgradeProductService)
    {
        _accountUpgradeProductRepository = accountUpgradeProductRepository;
        _accountUpgradeProductService = accountUpgradeProductService;
    }

    public async Task HandleAsync(SeedAccountUpgradeProducts command)
    {
        var existingAccountUpgradeProducts = await _accountUpgradeProductRepository.FindAllAsync();

        var newAccountUpgradeProducts =
            _accountUpgradeProductService.CreateInitialProducts(existingAccountUpgradeProducts);

        await _accountUpgradeProductRepository.AddRangeAsync(newAccountUpgradeProducts);
    }
}