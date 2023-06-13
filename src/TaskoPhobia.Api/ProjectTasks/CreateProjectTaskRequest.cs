using System.ComponentModel.DataAnnotations;
using TaskoPhobia.Application.Commands;

namespace TaskoPhobia.Api.ProjectTasks;

public class CreateProjectTaskRequest
{
    [Required]
    [MaxLength(50)]
    // #CR jak wcześniej wszystko
    public string TaskName { get; init; }
    
    [Required]
    public DateTime Start { get; init; }
    
    [Required]
    public DateTime End { get; init; }

    public CreateProjectTask ToCommand(Guid userId, Guid projectId)
    {
        return new CreateProjectTask(Guid.NewGuid(), projectId, userId, TaskName, Start, End);
    }
}