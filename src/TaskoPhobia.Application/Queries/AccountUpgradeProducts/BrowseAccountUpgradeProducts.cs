using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.AccountUpgradeProducts;

public record BrowseAccountUpgradeProducts : IQuery<IEnumerable<AccountUpgradeProductDto>>
{
}