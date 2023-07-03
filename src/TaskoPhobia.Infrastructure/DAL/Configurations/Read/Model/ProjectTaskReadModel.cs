namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

public class ProjectTaskReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public ProjectReadModel Project { get; set; }
}