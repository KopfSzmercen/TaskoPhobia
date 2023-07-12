namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class ProjectParticipationReadModel
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid ParticipantId { get; set; }
    public DateTimeOffset JoinDate { get; set; }
    public ProjectReadModel Project { get; set; }
}