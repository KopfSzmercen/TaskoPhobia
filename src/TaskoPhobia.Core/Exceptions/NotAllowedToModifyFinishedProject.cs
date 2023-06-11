using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class NotAllowedToModifyFinishedProject : CustomException
{
    public NotAllowedToModifyFinishedProject() : base("You're not allowed to modify a finished project.")
    {
    }
}