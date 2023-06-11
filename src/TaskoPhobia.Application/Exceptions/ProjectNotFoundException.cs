using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public class ProjectNotFoundException : CustomException
{
    public ProjectNotFoundException() : base("Project not found.")
    {
    }
}