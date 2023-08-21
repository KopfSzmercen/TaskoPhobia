using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Commands.AccountUpgradeProducts.OrderAccountUpgradeProduct.Exceptions;

public class AccountUpgradeProductNotFound : CustomException
{
    public AccountUpgradeProductNotFound() : base("Account upgrade product not found")
    {
    }
}