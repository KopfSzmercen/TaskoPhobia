using System.ComponentModel.DataAnnotations;
using TaskoPhobia.Application.Commands;

namespace TaskoPhobia.Api.Projects;

public class CreateProjectRequest
{
    // #CR podczas gdy klasa requestowa może być w api, to raczej jednak lepiej gdy jej tu nie ma, moja sugestia to albo zrezygnowanie z tego requestu i użycie handlera bezpośrednio, albo przeniesienie do application
    [Required]
    // #CR nie korzystamy zazwyczaj z atrybutów, jest to podejśnie przestarzałe. Zdecydowanie lepiej użyć FluentValidator
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