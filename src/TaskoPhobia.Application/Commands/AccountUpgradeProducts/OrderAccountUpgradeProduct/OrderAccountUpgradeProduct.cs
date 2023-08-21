using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.AccountUpgradeProducts.OrderAccountUpgradeProduct;

public sealed record OrderAccountUpgradeProduct(Guid ProductId) : ICommand;