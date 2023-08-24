namespace TaskoPhobia.Application.DTO;

public class AccountUpgradeProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string UpgradeTypeValue { get; init; }
    public int Amount { get; init; }
    public string Currency { get; init; }
    public string Description { get; init; }
}