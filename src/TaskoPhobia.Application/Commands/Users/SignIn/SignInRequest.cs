namespace TaskoPhobia.Application.Commands.Users.SignIn;

public class SignInRequest
{
    public string Email { get; init; }
    public string Password { get; init; }

    public SignIn ToCommand()
    {
        return new SignIn(Email, Password);
    }
}