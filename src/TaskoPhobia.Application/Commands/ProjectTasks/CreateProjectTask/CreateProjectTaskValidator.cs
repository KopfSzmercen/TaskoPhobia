using FluentValidation;

namespace TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;

internal sealed class CreateProjectTaskValidator : AbstractValidator<CreateProjectTask>
{
    public CreateProjectTaskValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.End)
            .NotEmpty();
        
        RuleFor(x => x.Start)
            .NotEmpty()
            .LessThan(x => x.End);

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.TaskName)
            .MinimumLength(2)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.TaskId)
            .NotEmpty();
    }
}