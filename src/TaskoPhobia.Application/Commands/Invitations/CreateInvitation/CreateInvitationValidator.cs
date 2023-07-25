using FluentValidation;

namespace TaskoPhobia.Application.Commands.Invitations.CreateInvitation;

internal sealed class CreateInvitationValidator : AbstractValidator<CreateInvitation>
{
    public CreateInvitationValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.InvitationId)
            .NotEmpty();

        RuleFor(x => x.ReceiverId)
            .NotEmpty();
    }
}