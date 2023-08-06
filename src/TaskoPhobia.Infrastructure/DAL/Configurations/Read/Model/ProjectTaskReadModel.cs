namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class ProjectTaskReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AssignmentsLimit { get; set; }
    public string Status { get; set; }
    public ProjectReadModel Project { get; set; }
    public Guid ProjectId { get; set; }
    public IReadOnlyList<ProjectTaskAssignmentReadModel> Assignments { get; set; }
}