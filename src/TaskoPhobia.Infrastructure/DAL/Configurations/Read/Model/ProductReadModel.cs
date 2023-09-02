namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class ProductReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Amount { get; init; }
    public string Currency { get; init; }
    public string Description { get; init; }
}