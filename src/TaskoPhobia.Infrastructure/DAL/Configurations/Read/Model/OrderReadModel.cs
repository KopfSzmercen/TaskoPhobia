namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

public class OrderReadModel
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string Currency { get; set; }
}