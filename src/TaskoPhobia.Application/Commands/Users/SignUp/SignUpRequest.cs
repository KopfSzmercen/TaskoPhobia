using System.ComponentModel.DataAnnotations;

namespace TaskoPhobia.Application.Commands.Users.SignUp;

public class SignUpRequest
{
    
    public string Email { get; init; }

    public string Username { get; init; }

    public string Password { get; init; }

    public SignUp ToCommand()
    {
        return new SignUp(Guid.NewGuid(), Email, Username, Password);
    }
}