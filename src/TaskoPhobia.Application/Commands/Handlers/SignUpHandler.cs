using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    public SignUpHandler(IUserRepository userRepository, IUserReadService userReadService,IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _userReadService = userReadService;
        _passwordManager = passwordManager;
    }

    private readonly IUserRepository _userRepository;
    private readonly IUserReadService _userReadService;
    private readonly IPasswordManager _passwordManager;

    public async Task HandleAsync(SignUp command)
    {
        
        
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(_passwordManager.Secure(command.Password));
        var role = new Role(Role.User());
        
        if (await _userReadService.ExistsByEmailAsync(email)) throw new EmailExistsException();
        if (await _userReadService.ExistsByUsernameAsync(username)) throw new UsernameExistsException();

        var user = new User(userId, email, username, password, role, DateTime.UtcNow, AccountType.Free());
        await _userRepository.AddAsync(user);

    }
}