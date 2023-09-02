namespace TaskoPhobia.Application.DTO;

public class OrderDto
{
    public Guid Id { get; init; }
    public PriceDto Price { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public Guid ProductId { get; init; }
}