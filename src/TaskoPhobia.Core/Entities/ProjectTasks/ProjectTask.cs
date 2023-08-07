using TaskoPhobia.Core.Common.Rules;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks.Rules;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.ProjectTasks;

public class ProjectTask : Entity
{
    private readonly ICollection<TaskAssignment> _assignments = new List<TaskAssignment>();

    private ProjectTask(ProjectTaskId id, ProjectTaskName name, TaskTimeSpan timeSpan, ProjectId projectId,
        ProgressStatus status, TaskAssignmentsLimit assignmentsLimit)
    {
        Id = id;
        Name = name;
        TimeSpan = timeSpan;
        ProjectId = projectId;
        Status = status;
        AssignmentsLimit = assignmentsLimit;
    }

    public ProjectTask()
    {
    }

    public ProjectTaskId Id { get; }
    public ProjectTaskName Name { get; private set; }
    public TaskTimeSpan TimeSpan { get; private set; }
    public ProgressStatus Status { get; private set; }
    public ProjectSummary Project { get; init; }
    public ProjectId ProjectId { get; private set; }
    public IEnumerable<TaskAssignment> Assignments => _assignments;
    public TaskAssignmentsLimit AssignmentsLimit { get; }

    public static ProjectTask CreateNew(ProjectTaskId id, ProjectTaskName name, TaskTimeSpan timeSpan,
        TaskAssignmentsLimit assignmentsLimit,
        Project project)
    {
        CheckRule(new FinishedProjectCanNotBeModifiedRule(project));
        return new ProjectTask(id, name, timeSpan, project.Id, ProgressStatus.InProgress(), assignmentsLimit);
    }

    public void AddAssignee(TaskAssignmentId id, UserId assigneeId, bool assigneeParticipatesProject)
    {
        CheckRule(new FinishedTaskMustNotBeModifiedRule(Status));
        CheckRule(new FinishedProjectCanNotBeModifiedRule(Project));
        CheckRule(new TaskAssigneeMustBeUniqueRule(assigneeId, _assignments));
        CheckRule(new TaskAssignmentsLimitMustNotBeExceededRule(_assignments, AssignmentsLimit));
        CheckRule(new AssigneeMustParticipateProjectRule(assigneeParticipatesProject));

        var newAssignment = TaskAssignment.New(id, assigneeId, Id);
        _assignments.Add(newAssignment);
    }

    public void Finish(UserId idOfUserWhoSetsTaskStatusToFinished)
    {
        CheckRule(new FinishedTaskMustNotBeModifiedRule(Status));
        CheckRule(new TaskCanBeSetToFinishedOnlyByItsAssigneeRule(_assignments, idOfUserWhoSetsTaskStatusToFinished));
        Status = ProgressStatus.Finished();
    }
}