namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class ProjectReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid OwnerId { get; set; }
    public IReadOnlyCollection<ProjectTaskReadModel> Tasks { get; set; }
}