using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Users.SignIn;

public sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    private readonly IUserReadService _userReadService;

    public SignInHandler(IAuthenticator authenticator,
        IPasswordManager passwordManager, ITokenStorage tokenStorage, IUserReadService userReadService)
    {
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
        _userReadService = userReadService;
    }


    public async Task HandleAsync(SignIn command)
    {
        var user = await _userReadService.GetByEmailAsync(command.Email);
        if (user is null) throw new InvalidCredentialsException();

        if (!_passwordManager.Validate(command.Password, user.Password)) throw new InvalidCredentialsException();

        var jwt = _authenticator.CreateToken(user.Id, user.Role);

        _tokenStorage.Set(jwt);
    }
}