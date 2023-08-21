namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class AccountUpgradeProductReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string UpgradeTypeValue { get; init; }
    public int Price { get; init; }
    public string Currency { get; init; }
    public string Description { get; init; }
}