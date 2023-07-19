using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.Entities;

public class ProjectParticipation
{
    private ProjectParticipation(ProjectParticipationId id, ProjectId projectId, UserId participantId,
        DateTimeOffset joinDate)
    {
        Id = id;
        ProjectId = projectId;
        ParticipantId = participantId;
        JoinDate = joinDate;
    }

    public ProjectParticipation()
    {
    }

    public ProjectParticipationId Id { get; }
    public ProjectId ProjectId { get; }
    public UserId ParticipantId { get; }
    public DateTimeOffset JoinDate { get; }
    public Project Project { get; init; }
    public User Participant { get; init; }

    public static ProjectParticipation CreateNew(ProjectId projectId, UserId participantId, IClock clock)
    {
        return new ProjectParticipation(new ProjectParticipationId(Guid.NewGuid()), projectId, participantId,
            clock.DateTimeOffsetNow());
    }
}