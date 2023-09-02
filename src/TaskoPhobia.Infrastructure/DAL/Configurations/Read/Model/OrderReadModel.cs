namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal class OrderReadModel
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string Currency { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public Guid ProductId { get; init; }
    public ProductReadModel Product { get; init; }
    public Guid CustomerId { get; set; }
}