using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public class ProjectTaskNotFoundException : CustomException
{
    public ProjectTaskNotFoundException() : base("Project task not found.")
    {
    }
}