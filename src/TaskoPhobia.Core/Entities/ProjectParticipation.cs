using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Time;

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

    public static ProjectParticipation CreateNew(ProjectId projectId, UserId participantId, IClock clock)
    {
        return new ProjectParticipation(projectId, participantId, clock.DateTimeOffsetNow());
    }
}