using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Handlers;

public class SignUpHandler : ICommandHandler<SignUp>
{
    public SignUpHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private readonly IUserRepository _userRepository;
    public async Task HandleAsync(SignUp command)
    {
        
        
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var role = new Role(Role.User());

        var userWithSameEmail = await _userRepository.GetByEmailAsync(email);
        if (userWithSameEmail is not null) throw new EmailExistsException();
        
        var userWithSameUsername = await _userRepository.GetByUsernameAsync(username);
        if (userWithSameUsername is not null) throw new UsernameExistsException();

        var user = new User(userId, email, username, password, role, DateTime.UtcNow);
        await _userRepository.AddAsync(user);

    }
}