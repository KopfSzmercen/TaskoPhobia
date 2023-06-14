using FluentValidation;

namespace TaskoPhobia.Application.Commands.Projects.CreateProject;

internal sealed class CreateProjectValidator : AbstractValidator<CreateProject>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.ProjectName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
        
        RuleFor(x => x.ProjectDescription)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(500);

        RuleFor(x => x.OwnerId)
            .NotEmpty();
        
        RuleFor(x => x.ProjectId)
            .NotEmpty();
    }
}