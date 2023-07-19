using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Users.SignUp;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IPasswordManager _passwordManager;
    private readonly IUserReadService _userReadService;

    private readonly IUserRepository _userRepository;

    public SignUpHandler(IUserRepository userRepository, IUserReadService userReadService,
        IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _userReadService = userReadService;
        _passwordManager = passwordManager;
    }

    public async Task HandleAsync(SignUp command)
    {
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(_passwordManager.Secure(command.Password));

        if (await _userReadService.ExistsByEmailAsync(email)) throw new EmailExistsException();
        if (await _userReadService.ExistsByUsernameAsync(username)) throw new UsernameExistsException();

        var user = new User(command.UserId, email, username, password, Role.User(), DateTime.UtcNow,
            AccountType.Free());
        await _userRepository.AddAsync(user);
    }
}