using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.Invitations.CreateInvitation;

internal sealed class CreateInvitationHandler : ICommandHandler<CreateInvitation>
{
    private readonly IClock _clock;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public CreateInvitationHandler(IUserRepository userRepository, IProjectRepository projectRepository, IClock clock)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateInvitation command)
    {
        var sender = await _userRepository.GetByIdAsync(command.SenderId);
        if (sender is null) throw new UserNotFoundException(command.SenderId);

        var receiver = await _userRepository.GetByIdAsync(command.ReceiverId);
        if (receiver is null) throw new UserNotFoundException(command.ReceiverId);

        var project = await _projectRepository.FindByIdAsync(command.ProjectId);
        if (project is null) throw new ProjectNotFoundException();

        var invitation = Invitation.CreateNew(command.InvitationId, $"Invitation for project: {project.Name}",
            command.SenderId, command.ReceiverId, _clock.Now());

        project.AddInvitation(invitation);

        await _projectRepository.UpdateAsync(project);
    }
}