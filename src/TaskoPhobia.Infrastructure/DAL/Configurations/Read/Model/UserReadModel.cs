namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

public class UserReadModel 
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}