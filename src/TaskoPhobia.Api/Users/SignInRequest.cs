using System.ComponentModel.DataAnnotations;
using TaskoPhobia.Application.Commands;

namespace TaskoPhobia.Api.Users;

public class SignInRequest
{
    // #CR jak wcześniej
    [Required]
    [EmailAddress]
    public string Email { get; init; }
    
    [Required]
    [MaxLength(50)]
    public string Password { get; init; }

    public  SignIn ToCommand()
    {
        return new SignIn(Email, Password);
    }
}