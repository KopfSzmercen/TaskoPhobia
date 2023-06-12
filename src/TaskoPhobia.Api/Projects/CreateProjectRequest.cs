using System.ComponentModel.DataAnnotations;
using TaskoPhobia.Application.Commands;

namespace TaskoPhobia.Api.Projects;

public class CreateProjectRequest
{
    [Required]
    [MaxLength(50)]
    public string ProjectName { get; init; }
    
    [Required]
    [MaxLength(50)]
    public string ProjectDescription { get; init; }

    public CreateProject ToCommand(Guid userId)
    {
        return new CreateProject(Guid.NewGuid(), ProjectName, ProjectDescription, userId);
    }
}