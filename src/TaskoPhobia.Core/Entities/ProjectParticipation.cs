using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class ProjectParticipation
{
    private ProjectParticipation(ProjectId projectId, UserId participantId, DateTimeOffset joinDate)
    {
        ProjectId = projectId;
        ParticipantId = participantId;
        JoinDate = joinDate;
    }

    public ProjectParticipation()
    {
    }

    public ProjectId ProjectId { get; }
    public UserId ParticipantId { get; }
    public DateTimeOffset JoinDate { get; }
    public Project Project { get; init; }
    public User Participant { get; init; }

    public static ProjectParticipation CreateNew(ProjectId projectId, UserId participantId)
    {
        return new ProjectParticipation(projectId, participantId, DateTimeOffset.Now);
    }
}