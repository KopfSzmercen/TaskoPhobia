using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.Users.SignUp;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserReadService _userReadService;
    private readonly IUserRepository _userRepository;

    public SignUpHandler(IUserRepository userRepository, IUserReadService userReadService,
        IPasswordManager passwordManager, IClock clock)
    {
        _userRepository = userRepository;
        _userReadService = userReadService;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task HandleAsync(SignUp command)
    {
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(_passwordManager.Secure(command.Password));

        if (await _userReadService.ExistsByEmailAsync(email)) throw new EmailExistsException();
        if (await _userReadService.ExistsByUsernameAsync(username)) throw new UsernameExistsException();

        var user = User.New(command.UserId, email, username, password, _clock.Now());
        await _userRepository.AddAsync(user);
    }
}